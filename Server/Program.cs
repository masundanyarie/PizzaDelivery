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

            IDatabase database = new DataBase();

            DriverServer driverServer = new DriverServer(database);
            server.AddService<IDriverServer, DriverServer>(driverServer);

            ManagerServer managerServer = new ManagerServer(database);
            server.AddService<IManagerServer, ManagerServer>(managerServer);

            server.Start();

            OrderEmulator emulator = new OrderEmulator(driverServer);
            emulator.stopTimer();

            Console.WriteLine("Enter:\n"
                    + "0 - exit\n"
                    + "2 - generate order\n");

            String line;
            do {
                //Wait user to stop server by pressing Enter
                line = Console.ReadLine();

                switch (line)
                {
                    case "1": 
                        break;
                    case "2":
                        emulator.generateOrder();
                        break;
                    case "3":
                        new Map();
                        break;
                }
            } while (!line.Equals("0"));

            //Stop server
            server.Stop();
        }
    }
}
