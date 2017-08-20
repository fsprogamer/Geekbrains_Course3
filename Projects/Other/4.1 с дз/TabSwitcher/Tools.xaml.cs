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

namespace TabSwitcher
{
    /// <summary>
    /// Interaction logic for ButtonsForEditing.xaml
    /// </summary>
    public partial class Tools : UserControl
    {
        public Tools()
        {
            InitializeComponent();
        }
        private string labelText = "";

        public string LabelText
        {
            get { return labelText; }
            set
            {
                labelText = value;
                label.Content = value;
            }
        }

        public static readonly DependencyProperty ComboboxSelectedItemProperty =
        DependencyProperty.Register("CbSelectedItem", typeof(object), typeof(Tools), new UIPropertyMetadata(""));

        public object CbSelectedItem
        {
            get { return (object)GetValue(ComboboxSelectedItemProperty); }
            set { SetValue(ComboboxSelectedItemProperty, value); }
        }

       

        public event RoutedEventHandler btnPlusClick;
        public event RoutedEventHandler btnMinusClick;
        public event RoutedEventHandler btnEditClick;



        private void btnAddSender_Click(object sender, RoutedEventArgs e)
        {
            btnPlusClick?.Invoke(sender, e);
        }

        private void btnEditSender_Click(object sender, RoutedEventArgs e)
        {
            btnEditClick?.Invoke(sender, e);
        }

        private void btnDeleteSender_Click(object sender, RoutedEventArgs e)
        {
            btnMinusClick?.Invoke(sender, e);
        }
    }
}
