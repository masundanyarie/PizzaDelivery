using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    class DataBase : IDatabase
    {
        public void put(DeliveryPizzaLib.Manager.Order order)
        {
            // stub
        }

        public List<DeliveryPizzaLib.Manager.Order> getSince(DateTime time)
        {
            // stub
            return new List<DeliveryPizzaLib.Manager.Order>();
        }

        public List<DeliveryPizzaLib.Manager.Order> getBetween(DateTime start, DateTime end)
        {
            // stub
            return new List<DeliveryPizzaLib.Manager.Order>();
        }

        public List<DeliveryPizzaLib.Manager.Order> getByDriverId(int id)
        {
            // stub
            return new List<DeliveryPizzaLib.Manager.Order>();
        }

        public List<DeliveryPizzaLib.Manager.Order> getByBranchId(int id)
        {
            // stub
            return new List<DeliveryPizzaLib.Manager.Order>();
        }

        public List<DeliveryPizzaLib.Manager.Order> getByPizzaType(int id)
        {
            // stub
            return new List<DeliveryPizzaLib.Manager.Order>();
        }
    }
}
