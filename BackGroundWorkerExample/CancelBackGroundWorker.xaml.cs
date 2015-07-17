using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace BackGroundWorkerExample
{
    /// <summary>
    /// Interaction logic for CancelBackGroundWorker.xaml
    /// </summary>
    public partial class CancelBackGroundWorker : Window
    {
        private BackgroundWorker worker = new BackgroundWorker();

        public CancelBackGroundWorker()
        {
            InitializeComponent();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                LabelStatus.Foreground = Brushes.Red;
                LabelStatus.Text = "Cancelled by user...";
            }
            else
            {
                LabelStatus.Foreground = Brushes.Green;
                LabelStatus.Text = "Done... Calc result: " + e.Result;
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                worker.ReportProgress(i);
                System.Threading.Thread.Sleep(250);
            }
            e.Result = 42;
        }


        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            worker.RunWorkerAsync();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            worker.CancelAsync();
        }
    }
}
