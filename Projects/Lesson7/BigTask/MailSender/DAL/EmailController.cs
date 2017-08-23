using Common.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MailSender.DAL
{
    public class EmailController:IEmailController
    {
        private readonly IEmailRepo _repository;
        private static SqlLocalDbContext _context = new SqlLocalDbContext();

        public SqlLocalDbContext Context {
            get { return _context;}            
        }        

        public EmailController(): this(_context)
        {
        }

        public EmailController(SqlLocalDbContext context)
        {
            _repository = new EmailRepo(context);            
        }

        public Email GetEmail(int id)
        {
            return _repository.GetItem(id);
        }

        public IList<Email> GetEmails()
        {
            return new List<Email>(_repository.GetItems());
        }
        
        public void InsertEmail(Email email)
        {
            _repository.InsertItem(email);
            _repository.Save();
        }

        public void DeleteEmail(int id)
        {
            _repository.DeleteItem(id);
            _repository.Save();
        }

        public void UpdateEmail(Email item)
        {
            _repository.UpdateItem(item);
            _repository.Save();
        }
        
    }    
}
