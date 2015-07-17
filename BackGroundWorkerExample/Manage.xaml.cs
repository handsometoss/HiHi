using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BackGroundWorkerExample
{
    /// <summary>
    /// Interaction logic for Manage.xaml
    /// </summary>
    public partial class Manage
    {
        public Manage()
        {
            InitializeComponent();
            InitScreen();
        }

        void InitScreen()
        {
            var type = typeof (IGroupable);
            var types =
                AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => type.IsAssignableFrom(p) && !p.IsInterface);
            foreach (var item in types)
            {
                var button = new Button();
                button.Content = item.Name;
                button.Click += button_Click;   
                button.Padding = new Thickness(10);
                MenuPanel.Children.Add(button);
            }


            
        }

        void button_Click(object sender, RoutedEventArgs e)
        {
            var sent = (Button) sender;
            var url = sent.Content + ".xaml";
            
        }
    }
}
