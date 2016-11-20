using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;
using Hik.Communication.ScsServices.Service;

namespace DeliveryPizzaLib.Manager
{
    [ScsService(Version = "1.0.0.0")]
    public interface IManagerServer
    {
        Statistics GetStatistics(DateTime startDate, DateTime endDate);
        Recommendations GetRecommendations();
    }
}
