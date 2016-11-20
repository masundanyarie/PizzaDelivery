using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Client;
using DeliveryPizzaLib.Driver;

namespace DriverApp
{
    public class DriverPresenter : IDriverClient, IDriverPresenter
    {
        private const int INVALID_DRIVER_ID = -1;
        private const string defaultIP = "127.0.0.1";
        private const int defaultPort = 10047;

        private IDriverView _view;
        private IScsServiceClient<IDriverServer> _scsClient;

        private int _driverId = INVALID_DRIVER_ID;

        public DriverPresenter(IDriverView view)
        {
            _view = view;
        }

        void IDriverPresenter.OnRegisterDriver()
        {
            try
            {
                _scsClient = ScsServiceClientBuilder.CreateClient<IDriverServer>(
                    new ScsTcpEndPoint(defaultIP, defaultPort), this);
                _scsClient.Connect();

                _driverId = _view.GetDriverId();

                if (_driverId != INVALID_DRIVER_ID)
                {
                    _scsClient.ServiceProxy.RegisterDriver(_driverId);
                    _view.OnConnected();
                }
                else
                {
                    _scsClient = null;
                    _view.OnDisconnected();
                }
            }

            catch (Exception)
            {
                _scsClient = null;
                _view.OnDisconnected();
            }
        }

        void IDriverPresenter.OnUnregisterDriver()
        {
            if (_scsClient != null)
            {
                _scsClient.ServiceProxy.RegisterDriver(_driverId);
                _scsClient.Disconnect();
                _scsClient = null;

                _view.OnDisconnected();
            }
        }

        void IDriverPresenter.OnPizzaDelivered()
        {
            if (_scsClient != null)
            {
                _scsClient.ServiceProxy.Delivered(_driverId);
            }
        }

        void IDriverPresenter.OnReadyForDelivery()
        {
            if (_scsClient != null)
            {
                _view.OnOrderReceived(_scsClient.ServiceProxy.GetRoute(_driverId));
            }
        }

        void IDriverClient.OnOrderReceived()
        {
            if (_scsClient != null && _driverId != INVALID_DRIVER_ID)
            {
                Route route = _scsClient.ServiceProxy.GetRoute(_driverId);

                if (route != null)
                {
                    _view.OnOrderReceived(route);
                }

            }
        }
    }
}
