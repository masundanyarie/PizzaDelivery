using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DeliveryPizzaLib.Manager;

namespace ManagerApp
{
    public partial class Form1 : Form, IManagerView
    {
        private IManagerPresenter _presenter;

        public Form1()
        {
            InitializeComponent();
            _presenter = new ManagerPresenter(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _presenter.OnGetRecommendations();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _presenter.OnGetStatistics();
        }

        public DateTime GetStartDate()
        {
            return DateTime.Now;
        }

        public DateTime GetEndDate()
        {
            return DateTime.Now;
        }

        public void OnStatisticsUpdate(Statistics stat)
        {
            if (stat == null)
            {
                Close();
            }
            // stub
        }

        public void OnRecommendationsUpdate(Recommendations recommend)
        {
            // stub
        }
    }
}
