using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTestMailSender
{
    [AddINotifyPropertyChangedInterface]
    public class SqlLocalDbContext : DbContext
    {
        public SqlLocalDbContext() : base("SqlLocalDatabaseConnection")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SqlLocalDbContext>());
        }
        public virtual DbSet<Email> Emails { get; set; }

        public virtual DbSet<EmailSender> SendersEmails { get; set; }

        public virtual DbSet<Mailing> Mailings { get; set; }
        public virtual DbSet<SmtpServer> SmtpServers { get; set; }

    }

}
