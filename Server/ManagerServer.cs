using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hik.Communication.ScsServices.Service;
using DeliveryPizzaLib.Manager;

namespace ServerApp
{
    class ManagerServer : ScsService, IManagerServer
    {
        IDatabase database = null;

        public Statistics GetStatistics(DateTime dateBegin, DateTime dateEnd)
        {
            Console.WriteLine("GetStatistics");
            if (database != null)
            {
                return new Statistics(database.getBetween(dateBegin, dateEnd).ToArray());
            }
            return new Statistics();
        }

        public Recommendations GetRecommendations()
        {
            Console.WriteLine("GetRecommendations");
            return new Recommendations();
        }
    }
}
