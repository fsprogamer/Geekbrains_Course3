using System;
using System.Windows;

namespace StartupUri
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        App()
        {
            InitializeComponent();
            
        }

        [STAThread]
        static void Main()
        {
            App app = new App();

            var mainWindow = new MainWindow();

            app.Run(mainWindow);
        }
    }
}
