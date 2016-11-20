using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPizzaLib.Driver
{
    public interface IDriverClient
    {
        void OnOrderReceived();
    }
}
