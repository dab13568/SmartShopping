using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;

namespace SmartShopping.StatisticsUC
{
    class StatisticsUserControlVM : INotifyPropertyChanged
    {
        private readonly BackgroundWorker worker = new BackgroundWorker();

        public StatisticsUserControlV View;

        private int _topic = -1;
        private int _time = -1;
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
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }

       
        private SeriesCollection _PieChartData;
        public SeriesCollection PieChartData
        {
            get { return _PieChartData; }
            set
            {
                _PieChartData = value;
                OnPropertyChanged("PieChartData");
            }
        }

        private SeriesCollection _LineChartData;
        public SeriesCollection LineChartData
        {
            get { return _LineChartData; }
            set
            {
                _LineChartData = value;
                OnPropertyChanged("LineChartData");
            }
        }

        private SeriesCollection _SticksChartData;
        public SeriesCollection SticksChartData
        {
            get { return _SticksChartData; }
            set
            {
                _SticksChartData = value;
                OnPropertyChanged("SticksChartData");
            }
        }

        private DateTime _firstDate = DateTime.Now;
        public DateTime firstDate
        {
            get { return _firstDate; }
            set
            {
                _firstDate = value;
                OnPropertyChanged("firstDate");
            }
        }

        private DateTime _secondDate = DateTime.Now;
        public DateTime secondDate
        {
            get { return _secondDate; }
            set
            {
                _secondDate = value;
                OnPropertyChanged("firstDate");
            }
        }


        private DateTime _OneDate = DateTime.Now;
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


        private Visibility _VisibilityProgressBar;
        public Visibility VisibilityProgressBar
        {
            get { return _VisibilityProgressBar; }
            set
            {
                _VisibilityProgressBar = value;
                OnPropertyChanged("VisibilityProgressBar");

            }
        }



        public void changeView()
        {
            if (time != -1 && topic != -1)
            {
                if (time == 2)
                {
                    if (firstDate != null && secondDate != null && firstDate <= secondDate)
                    {
                        
                        slice = new StatisticsUserControlM().getStatisticData(topic, time, firstDate, secondDate);
                    }
                    if (firstDate > secondDate)
                        View.InvalidDates.Visibility = Visibility.Visible;
                    else View.InvalidDates.Visibility = Visibility.Collapsed;
                }
                else
                {
                    slice = new StatisticsUserControlM().getStatisticData(topic, time, OneDate);
                }

            }
        }

        //private void worker_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    if ((int)e.Argument == 2)
        //        slice = new StatisticsUserControlM().getStatisticData(topic, time, firstDate, secondDate);
        //    else slice = new StatisticsUserControlM().getStatisticData(topic, time, OneDate);


        //    VisibilityProgressBar = Visibility.Visible;
        //}

        //private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    VisibilityProgressBar = Visibility.Collapsed;
        //}


        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            VisibilityProgressBar = Visibility.Visible;
            slice = new StatisticsUserControlM().getStatisticData(topic, time, OneDate,secondDate);
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            VisibilityProgressBar = Visibility.Collapsed;

        }


        public void LoadChart()
        {
            switch (type)
            {
                case 0:
                    LoadPieChart();
                    break;
                case 1:
                    LoadLineChart();
                    break;
                case 2:
                    LoadSticksChart();
                    break;
                default:
                    break;
            }
        }

        public void LoadPieChart()
        {
            if (PieChartData == null)
                PieChartData = new SeriesCollection();
            PieChartData.Clear();
            Func<ChartPoint, string> labelPoint = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            foreach (var n in slice)
            {
                PieChartData.Add(new PieSeries
                {
                    Title = n.Key,
                    Values = new ChartValues<double> { n.Value },
                    DataLabels = true,
                    LabelPoint = labelPoint
                });

            }
        }

        public Func<double, string> YFormatter { get; set; }

        private ObservableCollection<string> _Labels;
        public ObservableCollection<string> Labels
        {
            get { return _Labels; }
            set
            {
                _Labels = value;
                OnPropertyChanged("Labels");
            }
        }
        public void LoadLineChart()
        {
            if (LineChartData == null)
                LineChartData = new SeriesCollection();
            LineChartData.Clear();
            if(topic == 3)
            View.yAxis.Title = "עלות כספית";
            else View.yAxis.Title = "כמויות";

            ChartValues<double> chartValues = new ChartValues<double>();
            Labels = new ObservableCollection<string>();

            foreach (var n in slice)
            {
                chartValues.Add(n.Value);
            }

            LineChartData.Add
             (
                 new LineSeries
                 {
                     Values = chartValues,
                     Title = " "

                 }
             );

            foreach (var n in slice)
            {
                Labels.Add(n.Key);
            }
            YFormatter = value => value.ToString();


        }

        public void LoadSticksChart()
        {
            if (SticksChartData == null)
                SticksChartData = new SeriesCollection();
            SticksChartData.Clear();
            if (topic == 3)
                View.yAxisSticks.Title = "עלות כספית";
            else View.yAxisSticks.Title = "כמויות";

            ChartValues<double> chartValues = new ChartValues<double>();
            Labels = new ObservableCollection<string>();

            foreach (var n in slice)
            {
                chartValues.Add(n.Value);
            }


            SticksChartData.Add
             (
                 new ColumnSeries
                 {
                     Values = chartValues,
                     Title = " "

                 }
             );


            foreach (var n in slice)
            {
                Labels.Add(n.Key);
            }
            YFormatter = value => value.ToString();


        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {

            if (propertyName == "slice")
                LoadChart();

            else if (propertyName == "PieChartData" || propertyName == "Labels" || propertyName == "LineChartData" || propertyName == "SticksChartData")
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            else
                changeView();
        }
    }
}