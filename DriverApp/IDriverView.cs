using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DeliveryPizzaLib.Driver;

namespace DriverApp
{
    public interface IDriverView
    {
        void OnDisconnected();

        void OnConnected();

        void OnOrderReceived(Route route);

        int GetDriverId();
    }
}
