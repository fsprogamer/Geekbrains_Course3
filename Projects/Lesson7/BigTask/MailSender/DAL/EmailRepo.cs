using Common.Model;
using System;

namespace MailSender.DAL
{
    public class EmailRepo : GenericRepo<SqlLocalDbContext, Email >, IEmailRepo, IDisposable
    {
        public EmailRepo(SqlLocalDbContext context) : base(context)
        {
            
        }

        //public SqlLocalDbContext Context1
        //{
        //    get 
        //    {
        //        return Context; 
        //    }
        //}

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();                    
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }   
}
