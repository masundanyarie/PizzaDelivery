using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Client;
using DeliveryPizzaLib.Manager;

namespace ManagerApp
{
    public class ManagerPresenter : IManagerPresenter
    {
        private const string defaultIP = "127.0.0.1";
        private const int defaultPort = 10047;

        private IManagerView _view;
        private IManagerServer _server;

        public ManagerPresenter(IManagerView view)
        {
            _view = view;
        }

        public void OnGetStatistics()
        {
            var scsClient = ScsServiceClientBuilder.CreateClient<IManagerServer>(
                    new ScsTcpEndPoint(defaultIP, defaultPort), this);
            Statistics stat = scsClient.ServiceProxy.
                GetStatistics(_view.GetStartDate(), _view.GetEndDate());
            _view.OnStatisticsUpdate(stat);
        }

        public void OnGetRecommendations()
        {
            var scsClient = ScsServiceClientBuilder.CreateClient<IManagerServer>(
                    new ScsTcpEndPoint(defaultIP, defaultPort), this);
            Recommendations recommend = scsClient.ServiceProxy.
                GetRecommendations();
            _view.OnRecommendationsUpdate(recommend);
        }
    }
}
