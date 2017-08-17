using System;
using System.Windows;

namespace OnStartup
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var rnd = new Random();

            if (rnd.NextDouble() > 0.5)  
                    StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
            else
                    StartupUri = new Uri("SecondWindow.xaml",  UriKind.Relative);                    
            
        }
    }
}
