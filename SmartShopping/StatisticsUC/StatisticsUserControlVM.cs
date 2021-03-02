using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShopping.StatisticsUC
{
    class StatisticsUserControlVM
    {
        public StatisticsUserControlV View;

        private string _topic;
        private string _time;
        private string _type;
        public string topic
        { 
            get { return _topic; }
            set
            {
                _topic = value; 

            }
        }
        public string time { get { return _time; } set { _time = value; } }  
        public string type { get { return _type; } set { _type = value; } }
        public StatisticsUserControlVM(StatisticsUserControlV view)
        {
            this.View = view;
            AddSlice("grape", 2);
            AddSlice("watermelon", 3);
            AddSlice("orange", 4);
        }

        public SeriesCollection ChartData { get; }

        private Dictionary<string, double> _slice = new Dictionary<string, double>();

        public Dictionary<string, double> slice
        {
            get { return _slice; }
            set { _slice = value; }
        }

        public void AddSlice(string slicename, double slicevalue)
        {
            slice.Add(slicename, slicevalue);
        }

        public void changeView()
        {

        }
    }
}
