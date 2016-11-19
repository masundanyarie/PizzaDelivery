using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Client;
using DeliveryPizzaLib;

namespace DriverApp
{
    public partial class DriverAppForm : Form, IDriverClient
    {
        IDriverServer server;

        public DriverAppForm()
        {
            InitializeComponent();

            var client = ScsServiceClientBuilder.CreateClient<IDriverServer>(
                new ScsTcpEndPoint("127.0.0.1", 10048));

            client.Connect();
            server = client.ServiceProxy;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            server.RegisterDriver(7);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            server.GetRoute(7);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            server.Delivered(7);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            server.UnregisterDriver(7);
            
        }

        public void OnOrderReceived()
        {
            textBox1.Text += "1";
        }
    }
}
