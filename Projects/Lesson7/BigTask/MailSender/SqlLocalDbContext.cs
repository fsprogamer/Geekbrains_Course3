using Common.Model;
using System.Data.Entity;


namespace MailSender
{
    public class SqlLocalDbContext : DbContext
    {
        public SqlLocalDbContext() : base("EFMailsAndSendersConnectionString")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SqlLocalDbContext>());
        }
        public virtual DbSet<Email> Emails { get; set; }
    }
}
