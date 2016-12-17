using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPizzaLib.Manager
{
    [Serializable]
    public class Statistics
    {
        Order[] orders;

        public Statistics(Order[] orders)
        {
            this.orders = orders;
        }
        public Statistics()
        {
            this.orders = null;
        }
    }
}
