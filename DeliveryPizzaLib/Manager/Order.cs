using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPizzaLib.Manager
{
    [Serializable]
    public class Order
    {
        public DateTime OrderTime;
        public DateTime? DeliveredTime;
        public int? DriverId;
        public int? BranchILocationId;
        public int PizzaType;
        public int LocationId;
        public Order(DateTime orderTime, DateTime? deliveredTime, int? driverId, int? branchId, int pizzaType, int locationId)
        {
            OrderTime = orderTime;
            DeliveredTime = deliveredTime;
            DriverId = driverId;
            BranchILocationId = branchId;
            PizzaType = pizzaType;
            LocationId = locationId;
        }

        override public String ToString()
        {
            return "[" + OrderTime.ToString() + ", " + (DeliveredTime.HasValue?DeliveredTime.ToString():"null") + ", " + 
                (DriverId.HasValue?DriverId.ToString():"null") + ", " + 
                (BranchILocationId.HasValue?BranchILocationId.ToString():"null") + ", " + 
                PizzaType.ToString() + ", " +
                LocationId.ToString() + "]";
        }
    }
}
