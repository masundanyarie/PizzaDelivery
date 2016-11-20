using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerApp
{
    public interface IManagerPresenter
    {
        void OnGetStatistics();

        void OnGetRecommendations();
    }
}
