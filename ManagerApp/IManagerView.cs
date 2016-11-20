using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DeliveryPizzaLib.Manager;

namespace ManagerApp
{
    public interface IManagerView
    {
        DateTime GetStartDate();

        DateTime GetEndDate();

        void OnStatisticsUpdate(Statistics stat);

        void OnRecommendationsUpdate(Recommendations recommend);
    }
}
