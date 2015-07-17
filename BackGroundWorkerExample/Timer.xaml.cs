using System;
using System.Windows;
using System.Windows.Threading;

namespace BackGroundWorkerExample
{
    /// <summary>
    /// Interaction logic for Timer.xaml
    /// </summary>
    public partial class Timer : IGroupable
    {
        public Timer()
        {
            InitializeComponent();
            var timer = new DispatcherTimer {Interval = TimeSpan.FromMilliseconds(1)};
            timer.Tick += TickCount;
            timer.Start();
        }

        void TickCount(object sender, EventArgs e)
        {
            TimeLabel.Content = DateTime.Now.ToString("HH:mm:ss.fff");
        }
    }
}
