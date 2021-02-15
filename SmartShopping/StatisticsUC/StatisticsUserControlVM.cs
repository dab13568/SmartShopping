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

        public StatisticsUserControlVM(StatisticsUserControlV view)
        {
            this.View = view;
            AddSlice("grape", 2);
            AddSlice("watermelon", 3);
            AddSlice("orange", 4);
        }

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
    }
}
