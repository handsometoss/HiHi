using System;
using System.Windows;
using System.Windows.Controls;
using BackGroundWorkerExample.StrategyPattern.WithoutPattern;

namespace BackGroundWorkerExample.StrategyPattern
{
    /// <summary>
    /// Interaction logic for StrategyPattern.xaml
    /// </summary>
    public partial class StrategyPattern : IGroupable
    {
        private NoStrategyCharacter _noStrategyCharacter;

        public StrategyPattern()
        {
            InitializeComponent();
            _noStrategyCharacter= new NoStrategyCharacter();
            GenerateButton();

        }

        private void GenerateButton()
        {
            var units = Enum.GetValues(typeof (UnitType));
            foreach (var item in units)
            {
                var button = new Button {Content = item.ToString()};

                button.Click += ClickAction;
                
                
                button.Padding = new Thickness(10);
                UnitPanel.Children.Add(button);
            }
        }

        private void ClickAction(object sender, RoutedEventArgs args)
        {
            var sent = (Button)sender;
            var unitName = sent.Content.ToString();
            var type = Enum.Parse(typeof(UnitType), unitName);

            _noStrategyCharacter.CastSkill((UnitType)type);
        }
    }
}
    