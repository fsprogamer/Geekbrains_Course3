using System.Collections.Generic;
using Common.Model;
using System.Collections.ObjectModel;

namespace MailSender.DAL
{
    public interface IEmailController
    {
        
        Email GetEmail(int id);        
        IList<Email> GetEmails();
        void InsertEmail(Email item);
        void DeleteEmail(int id);
        void UpdateEmail(Email item);
    }
}
