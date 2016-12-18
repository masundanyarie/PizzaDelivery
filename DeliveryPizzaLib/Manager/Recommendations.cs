using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryPizzaLib.Manager
{
    [Serializable]
    public class Recommendations
    {
        KeyValuePair<int, int>[] replacements;

        public Recommendations(KeyValuePair<int, int>[] replacements)
        {
            this.replacements = replacements;
        }

        public override string ToString()
        {
            string s = "[ ";
            foreach (KeyValuePair<int, int> keypair in replacements)
            {
                s += "(" + keypair.Key + "=>" + keypair.Value + ")";
            }
            s += " ]";
            return s;
        }
    }
}
