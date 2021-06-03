using System;
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
using System.Windows.Threading;

namespace ITStepcalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();

            timer.Interval = new TimeSpan(0, 1, 0);
            timer.Tick += Timer_Tick;
        }
        double outgoing_amount = 0;
        long minute;
        private void Timer_Tick(object sender, EventArgs e)
        {
            ++minute;
            Times.Content = minute + $" minute";
            outgoing_amount += GetUanForMinute(13980, 166, 2, 90);
            Outgoing.Content = outgoing_amount.ToString() + $" uan";
        }

        public double GetUanForMinute(int total_amount, int total_schedule_dependency_amount, int count_schedule_in_days, int time_one_schedule)
        {
            double schedule = total_amount / total_schedule_dependency_amount;
            double uan_for_allschedules = schedule * count_schedule_in_days;
            return uan_for_allschedules / (time_one_schedule * 2);
        }
        public double GetOutgoingUan(int minute)
        {
            return GetUanForMinute(13980, 166, 2, 90) * minute;
        }
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
            Clear.IsEnabled = false;
            Times.Content = minute + $" minute";
            Outgoing.Content = outgoing_amount.ToString() + $" uan";
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            Clear.IsEnabled = true;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            outgoing_amount = 0;
            minute = 0;
            Times.Content = minute + $" minute";
            Outgoing.Content = outgoing_amount.ToString() + $" uan";
        }

        private void Calc_Click(object sender, RoutedEventArgs e)
        {
            Outgoing_Calc.Content = GetOutgoingUan(int.Parse(OutgoingMinutes.Text)).ToString();
        }

        private void OutgoingMinutes_SelectionChanged(object sender, RoutedEventArgs e)
        {
            int min = 0;
            if (int.TryParse(OutgoingMinutes.Text, out min))
            {
                Outgoing_Calc.Content = GetOutgoingUan(min).ToString();
            }
        }
    }

}
