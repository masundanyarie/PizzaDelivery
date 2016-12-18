using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DeliveryPizzaLib.Manager;

namespace ServerApp
{
    interface IDatabase
    {
        void put(Order order);
        List<Order> getSince(DateTime time);
        List<Order> getBetween(DateTime start, DateTime end);
        List<Order> getByDriverId(int id);
        List<Order> getByBranchId(int id);
        List<Order> getByPizzaType(int id);
    }
}
