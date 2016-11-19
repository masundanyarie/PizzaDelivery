using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Service;
using DeliveryPizzaLib;

namespace ServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = ScsServiceBuilder.CreateService(new ScsTcpEndPoint(10048));

            server.AddService<IDriverServer, Server>(new Server());
            server.Start();

            //Wait user to stop server by pressing Enter
            Console.WriteLine(
                "Press enter to send OnOrderReceived");
            Console.ReadLine();



            //Stop server
            server.Stop();
        }
    }
}
