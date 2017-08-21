using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTestMailSender;

namespace WpfTestMailSender
{
    /// <summary>
    /// Класс который отвечает за работу с базой данных
    /// </summary>
    public class DBClass
    {
        private EmailsDataContext emails = new EmailsDataContext();
        public IQueryable<Emails> Emails
        {
            get
            {
                return from c in emails.Emails select c;
            }
        }

    }

}
