using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Documents;

namespace BackGroundWorkerExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow :IGroupable
    {
        private const int Max = 10000;
        List<int> list = new List<int>()
        {
           1,2
        };
        public MainWindow()
        {
            InitializeComponent();
            
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Im Catching exceptions from ALL THREADS ^^ !!!!!!");
        }

        void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Somethings is not right here (_.__!) ....!!");
            e.Handled = true;
        }

        bool method()
        {
            
            int x = 1;
            int y = 0;
            int a = x/y;
            return false;
        }
        private void SynchronousCalculation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //var a = list.Where(x =>
                //{
                //    try
                //    {
                        
                //        this.method();
                //        return true;
                //    }
                //    catch (Exception)
                //    {
                //        return false;
                //        throw;
                //    }
                    
                //}).ToList();
                 list.Select(m => {
                    int x = 1;
                    int y = 0;
                    int a1 = x / y;
                                            return a1;
                }
                    ).ToList();

            }
            catch (Exception)
            {

                MessageBox.Show("Error");
                
            }
            
            //Reset();
            //var result = 0;
            //for (int i = 0; i < Max; i++)
            //{
            //    if (i % 42 == 0)
            //    {
            //        ResultsListBox.Items.Add(i);
            //        result++;
            //    }
            //    Thread.Sleep(1);
            //    CalculationProgressBar.Value = Convert.ToInt32(((double)i / Max) * 100);
            //}
        }


        private void AsynchronousCalculation_Click(object sender, RoutedEventArgs e)
        {
            //throw new OutOfMemoryException();
            //Reset();
            //LogInfo.Logging();
            var worker = new BackgroundWorker { WorkerReportsProgress = true };
            worker.DoWork += DoCalculation;
            worker.ProgressChanged += UpdateUI;
            worker.RunWorkerCompleted += CalculationComplete;
            worker.RunWorkerAsync(Max);
        }

        private void CalculationComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Done!!");
        }

        private void UpdateUI(object sender, ProgressChangedEventArgs e)
        {
            CalculationProgressBar.Value = e.ProgressPercentage;
            if (e.UserState != null)
                ResultsListBox.Items.Add(e.UserState);
        }

        private void DoCalculation(object sender, DoWorkEventArgs e)
        {
            var max = (int)e.Argument;
            var result = 0;
            //Dispatcher.Invoke(Reset);
            Reset();
            
            for (int i = 0; i < max; i++)
            {
                var progressPercentage = Convert.ToInt32(((double)i / max) * 100);
                if (i % 42 == 0)
                {
                    result++;
                    (sender as BackgroundWorker).ReportProgress(progressPercentage, i);
                }
                else
                {
                    (sender as BackgroundWorker).ReportProgress(progressPercentage);
                    Thread.Sleep(1);
                }

                e.Result = result;
            }


        }

        private void Reset()
        {
            CalculationProgressBar.Value = 0;
            ResultsListBox.Items.Clear();
        }

    }
}
