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
        private IDriverClient mClient;
        private OrderEmulator mEmulator;
        private IDatabase mDatabase;
        private ConcurrentQueue<Order> mOrderQueue;

        public DriverServer(IDatabase database)
        {
            mEmulator = new OrderEmulator(this);
            mDatabase = database;
            mOrderQueue = new ConcurrentQueue<Order>();
        }

        public int RegisterDriver(int driverId)
        {
            Console.WriteLine("RegisterDriver, driverId = " + driverId);
            mClient = CurrentClient.GetClientProxy<IDriverClient>();
            return 1;
        }

        public int UnregisterDriver(int driverId)
        {
            Console.WriteLine("UnregisterDriver, driverId = " + driverId);
            return 1;
        }

        public Route GetRoute(int driverId, int positionId)
        {
            Console.WriteLine("GetRoute, driverId = " + driverId);
            return new Route();
        }

        public void Delivered(int driverId)
        {
            Console.WriteLine("Delivered, driverId = " + driverId);
        }

        public void SendOnOrderReceived()
        {
            mClient.OnOrderReceived();
        }

        public void OnOrderReceived(Order order)
        {
            mOrderQueue.Enqueue(order);
            Console.WriteLine(order.ToString());
        }

    }
}
