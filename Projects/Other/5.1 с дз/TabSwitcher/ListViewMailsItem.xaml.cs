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
    /// Interaction logic for ListViewMailsItem.xaml
    /// </summary>
    public partial class ListViewMailsItem : UserControl
    {

        private string mailText = "";

        private string tbTimePickerText = "";
        public string TbTimePickerText
        {
            get { return tbTimePicker.Text; }
            set
            {
                tbTimePickerText = value;
                tbTimePicker.Text = value;
            }
        }

        public string MailText
        {
            get
            {
                return mailText;
            }

            set
            {
                mailText = value;
            }
        }

        public ListViewMailsItem()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler btnEditClick;
        public event RoutedEventHandler btnMinusClick;

        private void btnEditSender_Click(object sender, RoutedEventArgs e)
        {
            btnEditClick?.Invoke(sender, e);
        }

        private void btnDeleteSender_Click(object sender, RoutedEventArgs e)
        {
            btnMinusClick?.Invoke(sender, e);
        }

        private void tbTimePicker_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //TimeSpan tsSendTime = GetSendTime(tbTimePicker.Text);

            //if (tsSendTime == new TimeSpan())
            //{
            //    MessageBox.Show("Некорректный формат даты");
            //    return;
            //}
            //DateTime dtSendDateTime = (cldSchedulDateTimes.SelectedDate ?? DateTime.Today).Add(tsSendTime);
            //if (dtSendDateTime < DateTime.Now)
            //{
            //    MessageBox.Show("Дата и время отправки писем не могут быть раньше, чем настоящее время");
            //    return;
            //}
        }
        public TimeSpan GetSendTime(string strSendTime)
        {
            TimeSpan tsSendTime = new TimeSpan();
            try
            {
                tsSendTime = TimeSpan.Parse(strSendTime);
            }
            catch { }

            return tsSendTime;
        }
    }
}
