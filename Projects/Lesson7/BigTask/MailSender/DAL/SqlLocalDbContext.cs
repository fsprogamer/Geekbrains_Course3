using Common.Model;
using System.Data.Entity;


namespace MailSender.DAL
{
    
    public class SqlLocalDbContext : DbContext
    {
        public SqlLocalDbContext() : base("EFMailsAndSendersConnectionString")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SqlLocalDbContext>());
            //Database.Initialize(false);
        }
        public virtual DbSet<Email> Emails { get; set; }
    }
}
