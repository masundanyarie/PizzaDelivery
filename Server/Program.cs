using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Service;
using DeliveryPizzaLib.Driver;
using DeliveryPizzaLib.Manager;

namespace ServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = ScsServiceBuilder.CreateService(new ScsTcpEndPoint(10047));

            DriverServer driverServer = new DriverServer();
            server.AddService<IDriverServer, DriverServer>(driverServer);

            ManagerServer managerServer = new ManagerServer();
            server.AddService<IManagerServer, ManagerServer>(managerServer);

            server.Start();


            do {
                //Wait user to stop server by pressing Enter
                Console.WriteLine("Enter:\n"
                    + "0 - to exit\n"
                    + "1 - to send OnOrderReceived\n");
                String line = Console.ReadLine();

                switch (line)
                {
                    case "0":
                        break;
                    case "1": 
                        driverServer.SendOnOrderReceived();
                        break;
                    case "3":
                        new Map();
                        break;
                }
            } while(!driverServer.Equals("0"));

            //Stop server
            server.Stop();
        }
    }
}
