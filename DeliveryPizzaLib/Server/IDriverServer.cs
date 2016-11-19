using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hik.Communication.ScsServices.Service;

namespace DeliveryPizzaLib
{
    [ScsService(Version = "1.0.0.0")]
    public interface IDriverServer
    {
        int RegisterDriver(int driverId);
        int UnregisterDriver(int driverId);
        Route GetRoute(int driverId);
        void Delivered(int driverId);
    }
}
