using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Transactions;
using System.Windows;
using System.Windows.Threading;
using ChinhDo.Transactions;

namespace BackGroundWorkerExample
{
    /// <summary>
    /// Interaction logic for CopyTest.xaml
    /// </summary>
    public partial class CopyTest : Window
    {
        private readonly IFileManager _target;
        private TransactionScope scope;
        private bool isRunning;
        private int _theRandom;
        private static List<TransactionScope> TransactionScopes = new List<TransactionScope>();
        private BackgroundWorker copyBackgroundWorker = new BackgroundWorker();

        public CopyTest()
        {
            _target = new TxFileManager();
            InitializeComponent();
        }

        private void ButtonCopy_OnClick(object sender, RoutedEventArgs e)
        {
            _theRandom = new Random().Next();
            copyBackgroundWorker = new BackgroundWorker();
            copyBackgroundWorker.WorkerSupportsCancellation = true;
            copyBackgroundWorker.DoWork += Copy;
            copyBackgroundWorker.RunWorkerAsync();
        }

        private void Copy(object sender, DoWorkEventArgs e)
        {
            const string source = @"C:\Users\Hiep.LamTang.CTCPLC\Desktop\SampleFile\Pic1.jpg";
            var destination = string.Format(@"C:\Users\Hiep.LamTang.CTCPLC\Desktop\SampleFile\Des\TheTest{0}.jpg", _theRandom);
            const string desFarFarAway = @"\\lonfps04p\MSphere DEV Shared\Mock DFS\London\Documents\401\6013\In\9657.1.zip";

            using (scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                TransactionScopes.Add(scope);
                InsertToDatabase();
                _target.Copy(source, destination, true);
                Thread.Sleep(3000 * 60 * 10);
                
                scope.Complete();           
            }

        }

        private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            copyBackgroundWorker.CancelAsync();
        }

        private void InsertToDatabase()
        {
            const string constring = @"Data Source=(LocalDb)\v11.0;Initial Catalog=BetBet;Integrated Security=true;";
            using (var cn = new SqlConnection(constring))
            {
                var cmd = new SqlCommand("Insert into Products values('Test " + _theRandom + "')", cn);

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
           

        }

        private void ButtonDispose_OnClick(object sender, RoutedEventArgs e)
        {
            DisposeTransaction();
        }

        private void DisposeTransaction()
        {
            foreach (var transactionScope in TransactionScopes)
            {
                transactionScope.Dispose();
            }
        }


        private static List<BackgroundWorker> backgroundWorkers = new List<BackgroundWorker>();
        private DispatcherTimer timer;
        private void ButtonGenWorker_OnClick(object sender, RoutedEventArgs e)
        {
            if (timer == null)
            {
                var worker = new BackgroundWorker();
                worker.DoWork += StartTimer;
                worker.RunWorkerAsync();
            }
            
            WorkerIsWorking.Text = "Workers is : " + backgroundWorkers.Count;

            var backgroundworker = new BackgroundWorker();
            backgroundWorkers.Add(backgroundworker);

            backgroundworker.DoWork += DoWorkIn5Minute;
            backgroundworker.RunWorkerCompleted += DoWorkCompelte;

            backgroundworker.RunWorkerAsync(backgroundworker);
        }

        private void StartTimer(object sender, DoWorkEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Tick += (sender1, e1) =>
            {
                //PendingWorker.Text = string.Format("There is {0} pending worker(s)",
                //    backgroundWorkers.Count(x => x.IsBusy));
                while (backgroundWorkers.Any(x => !x.IsBusy))
                {

                }
            };
            timer.Interval = new TimeSpan(0, 0, 0, 7);
            timer.Start();
        }
        private void DoWorkCompelte(object sender, RunWorkerCompletedEventArgs e)
        {
            var result = e.Result as BackgroundWorker;
            if (result != null)
            {
                backgroundWorkers.Remove(result);    
            }
            
        }

        private void DoWorkIn5Minute(object sender, DoWorkEventArgs e)
        {
            var argument = e.Argument;
            Thread.Sleep(1000 * 5);
            e.Result = argument;
        }

    }
}
