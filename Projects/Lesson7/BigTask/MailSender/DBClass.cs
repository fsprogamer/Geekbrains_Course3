using System.Linq;
using Common.Model;
using System.Collections.Generic;
using MailSender.DAL;

namespace MailSender
{
    //public class DBClass
    //{
    //    private EmailsDataContext emails = new EmailsDataContext();
    //    public IQueryable<Emails> Emails
    //    {
    //        get
    //        {
    //            return from c in emails.Emails select c;
    //        }
    //    }
    //}

    /// <summary>
    /// Класс, который отвечает за работу с базой данных
    /// </summary>
    public class DBClass
    {
        private SqlLocalDbContext _сontext = new SqlLocalDbContext();

        public List<Email> Emails => _сontext.Emails.ToList();
    }
}
