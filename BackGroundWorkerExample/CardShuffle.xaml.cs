using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace BackGroundWorkerExample
{
    /// <summary>
    /// Interaction logic for CardShuffle.xaml
    /// </summary>
    public partial class CardShuffle:IGroupable
    {
        private int size = 10;
        List<int> tempList;
        List<int> resultList;
        Random random;

        public CardShuffle()
        {
            InitializeComponent();
        }

        private void FisherAndYatesButton_OnClick(object sender, RoutedEventArgs e)
        {
            var watch = Stopwatch.StartNew();
            InitData();

            while (tempList.Any())
            {
                var randomIndex = random.Next(0, tempList.Count);

                resultList.Add(tempList[randomIndex]);
                tempList.RemoveAt(randomIndex);
            }

            PopupResult(resultList);
            watch.Stop();
            var elapsed = watch.Elapsed;
            ElapsedTimeLabel.Content = elapsed.ToString();
        }

        private void InitData()
        {
            tempList = new List<int>();
            resultList = new List<int>();
            random = new Random();
            ResultLabel.Content = string.Empty;

            for (var i = 0; i < size; i++)
            {
                tempList.Add(i);
            }
        }

        private void MordernButton_OnClick(object sender, RoutedEventArgs e)
        {
            var watch = Stopwatch.StartNew();
            //0 1 2 3 4 5 6 7 8 9
            InitData();

            for (var i = tempList.Count - 1; i >= 0; i--)
            {
                var j = random.Next(0, i + 1);
                var temp = tempList[i];
                tempList[i] = tempList[j];
                tempList[j] = temp;
            }

            PopupResult(tempList);
            watch.Stop();
            var elapsed = watch.Elapsed;
            ElapsedTimeLabel.Content = elapsed.ToString();
        }

        private void PopupResult(List<int> results)
        {
            var result = string.Empty;
            foreach (var i in results)
            {
                result += i + " ";
            }
            ResultLabel.Content = "Result: " + result;
        }
    }
}
