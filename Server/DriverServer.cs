using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

using Hik.Communication.ScsServices.Service;
using DeliveryPizzaLib.Driver;
using DeliveryPizzaLib.Manager;

namespace ServerApp
{
    class DriverServer : ScsService, IDriverServer, OrderEmulator.OrerListener
    {
        private Dictionary<int, Driver> mDrivers = new Dictionary<int, Driver>();
        private HashSet<int> mBranchLocations = new HashSet<int>();
        private ConcurrentQueue<Order> mOrderQueue;
        private Map mMap = new Map();

        private IDatabase mDatabase;
        private int freeDrivers = 0;

        public DriverServer(IDatabase database)
        {
            mDatabase = database;
            mOrderQueue = new ConcurrentQueue<Order>();

            mBranchLocations.Add(22);
            mBranchLocations.Add(26);
            mBranchLocations.Add(62);
            mBranchLocations.Add(66);
            mBranchLocations.Add(44);
        }

        public int RegisterDriver(int driverId, int positionId)
        {
            if (!mDrivers.ContainsKey(driverId))
            {
                Console.WriteLine("Driver registred, driverId = " + driverId);
                mDrivers.Add(driverId, new Driver(driverId, CurrentClient.GetClientProxy<IDriverClient>(), positionId, true, null));
                freeDrivers++;
                SendOrders();
                return 0;
            }
            Console.WriteLine("Driver doesn't registred, driverId = " + driverId);
            return 1;
        }

        public int UnregisterDriver(int driverId)
        {
            if (mDrivers[driverId].order == null)
            {
                mDrivers.Remove(driverId);
                freeDrivers--;
                Console.WriteLine("Driver unregistred: " + driverId);
                return 0;
            }
            Console.WriteLine("Driver still has an order, driverId = " + driverId);
            return 1;
        }

        public Route GetRoute(int driverId)
        {
            if (mDrivers[driverId].order == null)
            {
                Console.WriteLine("No orders, driverId = " + driverId);
                return null;
            }

            Route route = mMap.getRoute(mDrivers[driverId].positionId, mDrivers[driverId].order.BranchILocationId.Value)
                .Concat(mMap.getRoute(mDrivers[driverId].order.BranchILocationId.Value, mDrivers[driverId].order.LocationId));
            route.pizzaType = mDrivers[driverId].order.PizzaType;
            Console.WriteLine("GetRoute, driverId = " + driverId + ", " + route.ToString());
            return route;
        }

        public void Delivered(int driverId)
        {
            if (mDrivers[driverId].order != null)
            {
                Order order = mDrivers[driverId].order;
                mDrivers[driverId].order = null;
                mDrivers[driverId].isFree = true;
                order.DeliveredTime = DateTime.Now;
                freeDrivers++;
                SendOrders();

                if (mDatabase != null)
                {
                    mDatabase.put(order);
                    //TODO write in database
                }

                Console.WriteLine("Drivered! " + order.ToString());
                return;
            }
            Console.WriteLine("Driver hasn't any orders, driverId = " + driverId);
        }

        public void OnOrderReceived(Order order)
        {
            Console.WriteLine("New order! " + order.ToString());
            mOrderQueue.Enqueue(order);
            SendOrders();
        }

        private void SendOrders()
        {
            Task.Factory.StartNew(
                () =>
                {                
                    while (freeDrivers != 0 && !mOrderQueue.IsEmpty)
                    {
                        Order order;
                        mOrderQueue.TryDequeue(out order);
                
                        int min = Int32.MaxValue;
                        int nearestBranch = 0;

                        foreach (int branchLocationId in mBranchLocations)
                        {
                            int tmp = mMap.getDist(order.LocationId, branchLocationId);
                            if (tmp < min)
                            {
                                min = tmp;
                                nearestBranch = branchLocationId;
                            }                        
                        }

                        min = Int32.MaxValue;
                        Driver nearestDriver = null;

                        foreach (Driver driver in mDrivers.Values)
                        {
                            if (driver.isFree)
                            {
                                int tmp = mMap.getDist(nearestBranch, driver.positionId);
                                if (tmp < min)
                                {
                                    min = tmp;
                                    nearestDriver = driver;
                                }
                            }
                        }

                        nearestDriver.order = order;
                        nearestDriver.isFree = false;
                        freeDrivers--;
                        order.DriverId = nearestDriver.id;
                        order.BranchILocationId = nearestBranch;

                        Console.WriteLine("Sent OnOrderReceived for driverId = " + nearestDriver.id);
                        nearestDriver.connection.OnOrderReceived();
                    }
                });
        }

        public void SendLocation(int driverId, int positionId)
        {
            mDrivers[driverId].positionId = positionId;
        }
    }

    class Driver
    {
        public int id;
        public Order order;
        public bool isFree;
        public int positionId;
        public IDriverClient connection;

        public Driver(int id, IDriverClient connection, int positionId, bool isFree, Order order)
        {
            this.id = id;
            this.connection = connection;
            this.positionId = positionId;
            this.isFree = isFree;
            this.order = order;
        }
    }
}
