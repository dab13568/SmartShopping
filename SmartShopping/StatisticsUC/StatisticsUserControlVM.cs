using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShopping.StatisticsUC
{
    class StatisticsUserControlVM : INotifyPropertyChanged
    {
        public StatisticsUserControlV View;

        private int _topic=-1;
        private int _time=-1;
        private int _type;
        public int topic
        { 
            get { return _topic; }
            set
            {
                _topic = value;
                OnPropertyChanged("topic");
            }
        }
        public int time
        {
            get { return _time; } 
            set 
            {
                _time = value;
                OnPropertyChanged("time");
            }
        }  
        public int type
        {
            get { return _type; } 
            set 
            {
                _type = value;
                OnPropertyChanged("type");
            }
        }
        public StatisticsUserControlVM(StatisticsUserControlV view)
        {
            this.View = view;
            //AddSlice("grape", 2);
            //AddSlice("watermelon", 3);
            //AddSlice("orange", 4);
        }

        private SeriesCollection _ChartData;
        public SeriesCollection ChartData 
        { get { return _ChartData; }
          set 
            {
                _ChartData = value;
                OnPropertyChanged("ChartData");
            }
        }

        private DateTime _firstDate;
        public DateTime firstDate
        {
            get { return _firstDate; }
            set 
            {
                _firstDate = value;
                OnPropertyChanged("firstDate");
            }
        }

        private DateTime _secondDate;
        public DateTime secondDate
        {
            get { return _secondDate; }
            set
            {
                _secondDate = value;
                OnPropertyChanged("firstDate");
            }
        }


        private DateTime _OneDate=DateTime.Now;
        public DateTime OneDate
        {
            get { return _OneDate; }
            set
            {
                _OneDate = value;
                OnPropertyChanged("firstDate");
            }
        }

        private Dictionary<string, float> _slice;
        public Dictionary<string, float> slice
        {
            get { return _slice; }
            set
            {
                _slice = value; 
                OnPropertyChanged("slice");
            }
        }

        public void AddSlice(string slicename, float slicevalue)
        {
            slice.Add(slicename, slicevalue);
        }

        public void changeView()
        {
            if(time!=-1 && topic!=-1)
            {
                if (time == 2)
                {
                    if (firstDate != null && secondDate != null && firstDate < secondDate)
                    {
                        slice = new StatisticsUserControlM().getStatisticData(topic, time, firstDate, secondDate);
                        //refreshChart();
                    }
                }
                else 
                {
                    slice = new StatisticsUserControlM().getStatisticData(topic, time, OneDate);
                    //refreshChart();
                }

            }
        }

        public void refreshChart()
        {
            if (ChartData == null)
                ChartData = new SeriesCollection();
            ChartData.Clear();
            Func<ChartPoint, string> labelPoint = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            foreach (var n in slice)
            {
                ChartData.Add(new PieSeries
                {
                    Title = n.Key,
                    Values = new ChartValues<double> { n.Value },
                    DataLabels = true,
                    LabelPoint = labelPoint
                });

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (propertyName == "slice")
                refreshChart();

            else if (propertyName == "ChartData")
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            else
                changeView(); 
        }
    }
}
