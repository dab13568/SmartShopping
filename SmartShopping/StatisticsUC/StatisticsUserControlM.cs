using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShopping.StatisticsUC
{
    class StatisticsUserControlM
    {
        public StatisticsUserControlM() { }

        public Dictionary<string, float> getStatisticData(int subject, int timePeriod, DateTime? dt1=null, DateTime? dt2=null)
        {
            return new BLimp().getStatisticData(subject, timePeriod, dt1, dt2);
        }
    }
}
