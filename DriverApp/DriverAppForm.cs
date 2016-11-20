using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DeliveryPizzaLib.Driver;

namespace DriverApp
{
    public partial class DriverAppForm : Form, IDriverView
    {
        private const int INVALID_DRIVER_ID = -1;

        private IDriverPresenter _presenter;

        public DriverAppForm()
        {
            InitializeComponent();
            _presenter = new DriverPresenter(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _presenter.OnRegisterDriver();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _presenter.OnPizzaDelivered();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _presenter.OnReadyForDelivery();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _presenter.OnUnregisterDriver();       
        }

        void IDriverView.OnDisconnected()
        {
            // stub
        }

        void IDriverView.OnConnected()
        {
            // stub
        }

        void IDriverView.OnOrderReceived(Route route)
        {
            if (route != null)
            {
                textBox1.Text += "1";
            }
            // stub
        }

        int IDriverView.GetDriverId()
        {
            return 1;
            // stub
        }
    }
}
