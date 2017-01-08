using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ServerApp
{
    class DataBase : IDatabase
    {
        public void put(DeliveryPizzaLib.Manager.Order order)
        {
            string[] pizzaSizes = { "S", "M", "L" };
            double[] pizzaPrices = { 7.00, 8.00, 10.00 };
            int x;
            double firstTotal;
            double grandTotal;
            double firstTotals;
            string[] pizzaTop = { "P", "O", "I", "B", "G" };
            double[] topPrices = { 0, 50 };
            int y;
            Console.WriteLine("Select your pizza size(Your choices are S, M or L)");
            string userPizzaSize = Console.ReadLine();
            Console.WriteLine("How many Pizzas would you like");
            double numPizzas = Cnovert.ToDouble(console.ReadLine());
            for (x = 0; x < pizzaSizes.Length; ++x)
            {
                if (userPizzaSize == pizzaSizes[x])
                {
                    firstTotal = numPizzas * pizzaPrices[x];
                    Console.WriteLine("You ordered {0} pizzas,numPizzas");
                    Console.WriteLine("Your total is {0:}", firstTotal);
                }
            }

            Console.WriteLine("Would you like any extra toppings for $0.50,", firstTotal);
            string top = Console.ReadLine();
            if (top == "y")
            {
                Console.WriteLine("\n Our toppings are :\n");
                Console.WriteLine("\n Select the first letter of your pizza toppings\n" + "(Your chjoices are P,O,I)");

                string userPizza = Console.ReadLine();
                Console.WriteLine("How many pizza toppings would you like?");
                double numTop = Convert.ToDouble(Console.ReadLine());
                for
                    (y = 0; y < pizzaTop.Length; ++y)
                {
                    if (userpizzaTop == pizzaTop[y])
                    {
                        firstTotals = numTop * .50;
                        firstTotal = numPizzas * pizzaPrices[x];
                        Console.WriteLine("You ordered {0}pizza topping.", numTop);
                        grandTotal = firstTotals + firstTotal;
                        Console.WriteLine("Your total will be {0:c}.", grandTotal);

                        // stub
                    }
                }
            }
        }

        public List<DeliveryPizzaLib.Manager.Order> getSince(DateTime time)
        {
            DateTime when =GetDateTimeinPast();
            TimeSpan ts = DateTime.Now.Subtract(when);
            if (ts.TotalHours < 1)
                b.AppendFormat ("{0} minutes ago", (int)ts.TotalMinutes);
            else if (ts.TotalDays <1 )
                b.AppendFormat("{0} hours ago", (int)ts.TotalHours);
            // stub
            return new List<DeliveryPizzaLib.Manager.Order>();
        }

        public List<DeliveryPizzaLib.Manager.Order> getBetween(DateTime start, DateTime end)
        {
            for (DateTime = StartingDate; date <= endingDate; date = date.AddDays(1))
                allDates.Add(1);
           // stub
            return new List<DeliveryPizzaLib.Manager.Order>();
        }

        public List<DeliveryPizzaLib.Manager.Order> getByDriverId(int id)
        { }
        public void OnOrderReceived(Order order)
        { }
                public int UnregisterDriver(int driverId)
        {
            if (mDrivers[driverId].order == null)
            {
                mDrivers.Remove(driverId);
                freeDrivers--;
                Console.WriteLine("Driver unregistred: " + driverId);
                return 0;
            }
            Console.WriteLine("Driver still has an order, driverId = " + driverId);
            return 1;
            Console.WriteLine("New order! " + order.ToString());
            mOrderQueue.Enqueue(order);
            SendOrders();
            // stub
            return new List<DeliveryPizzaLib.Manager.Order>();
        }

                public List<DeliveryPizzaLib.Manager.Order> getByBranchId(int id)

                { int branchId; }
        
             Transform.GetOder ClosestBranchId(Transform[]branch)
             {
                 Transform bestTarget= null;
                 float closestDistanceSqr =Mathf.Infinity;
                 vector3 currentPosition =transform.position;
                 foreach(Transform potentialTarget in branches)
                 {
                     vector3 directionToTarget = potentialTarget.position-currentPosition;
                     float dsqrTotarget= directionToTarget.sqrMagnitude;
                     if(dsqrToTarget <closestDistanceSqr)
                     {
                         closestDistanceSqr= dSqrToTarget;
                         bestTarget= potentialTarget;
                     }return bestTarget;
                 }
             
        
            
            // stub
            return new List<DeliveryPizzaLib.Manager.Order>();
        }

        public List<DeliveryPizzaLib.Manager.Order> getByPizzaType(int id)
        {
            PreparePizzaas(IList<IPizza>);
            {
                foreach (Ipizza pizza in pizaas)
                    pizza.Prepare();
            }

            // stub
            return new List<DeliveryPizzaLib.Manager.Order>();
        }
    }
}
