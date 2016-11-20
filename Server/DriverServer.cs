using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hik.Communication.ScsServices.Service;
using DeliveryPizzaLib.Driver;
using DeliveryPizzaLib.Manager;

namespace ServerApp
{
    class DriverServer : ScsService, IDriverServer
    {
        IDriverClient client;
        public int RegisterDriver(int driverId)
        {
            Console.WriteLine("RegisterDriver, driverId = " + driverId);
            client = CurrentClient.GetClientProxy<IDriverClient>();
            return 1;
        }

        public int UnregisterDriver(int driverId)
        {
            Console.WriteLine("UnregisterDriver, driverId = " + driverId);
            return 1;
        }

        public Route GetRoute(int driverId)
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
            client.OnOrderReceived();
        }
    }
}
