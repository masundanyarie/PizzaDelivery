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
                System.Console.WriteLine(route.ToString());
            }
            // stub
        }

        int IDriverView.GetDriverId()
        {
            return Int32.Parse(textBox1.Text);
        }
    }
}
