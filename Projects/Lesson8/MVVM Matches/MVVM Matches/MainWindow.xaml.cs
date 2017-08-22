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

namespace MVVM_Matches
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
      
        public MainWindow()
        {
            InitializeComponent();
            //Один из вариантов задания ViewModel - в коде как здесь. Чтобы полностью избавиться от кода,
            // используя нашу библиотеку MVVM Light сделаем ссылку в XAML файле нашего окна на статический ресурс, в котором будет ссылка на ViewModel
            //var vm = new MainViewModel();
            //this.DataContext = vm;
        }      

        
    }
}
