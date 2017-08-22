using Common.Model;
using System.Data.Entity;


namespace Common
{
    public class SqlLocalDbContext : DbContext
    {
        public SqlLocalDbContext() : base("SqlLocalDatabaseConnection")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SqlLocalDbContext>());
        }
        public virtual DbSet<Email> Emails { get; set; }
    }
}
