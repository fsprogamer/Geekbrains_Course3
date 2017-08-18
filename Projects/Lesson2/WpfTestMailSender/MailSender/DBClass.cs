using System.Linq;

namespace MailSender
{
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
