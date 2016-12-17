using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using DeliveryPizzaLib.Manager;

namespace ServerApp
{
    class OrderEmulator
    {
        public interface OrerListener
        {
            void OnOrderReceived(Order order);
        }

        private Thread mThread;
        private OrderEmulator.OrerListener mListener;
        private Random mRand = new Random(0);
        Timer timer;

        public OrderEmulator(OrerListener listener)
        {
            mListener = listener;
            //mThread = new Thread(this.Run);
            //mThread.Start();
            timer = new Timer(generateOrder, null, 0, 3000);
        }

        public void stopTimer()
        {
            timer.Change(1000000, 1000000);
        }

        public void startTimer()
        {
            timer.Change(0, 3000);
        }

        private void Run()
        {
            
            while (true)
            {

            }
        }

        public void generateOrder()
        {
            generateOrder(null);
        }

        private void generateOrder(object obj)
        {
            int size = Map.Size;
            Order order = new Order(DateTime.Now, null, null, null, mRand.Next(5), mRand.Next(size * size - 1));
            if (mListener != null)
            {
                mListener.OnOrderReceived(order);
            }
        }

    }
}
