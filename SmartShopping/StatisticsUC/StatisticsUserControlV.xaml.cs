﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;


namespace SmartShopping.StatisticsUC
{
    /// <summary>
    /// Interaction logic for StatisticsUserControlV.xaml
    /// </summary>
    public partial class StatisticsUserControlV : UserControl
    {
        public StatisticsUserControlV()
        {
            InitializeComponent();

            //Func<ChartPoint, string> labelPoint = chartPoint =>
            //    string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            //foreach (var n in (new StatisticsUserControlVM(this)).slice)
            //{
            //    PieChartProducts.Series.Add(new PieSeries
            //    {
            //        Title = n.Key,
            //        Values = new ChartValues<double> { n.Value },
            //        DataLabels = true,
            //        LabelPoint = labelPoint
            //    }) ;
                
            //}


            DataContext = new StatisticsUserControlVM(this);
        }

        public Func<ChartPoint, string> PointLabel { get; set; }

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {

            var chart = (PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }

        
    }
}
