using System.Data.Entity;
using EntityFrameworkApplication.Model;

namespace EntityFrameworkApplication
{
    public class SqlLocalDbContext : DbContext
    {
        public SqlLocalDbContext() : base("SqlLocalDatabaseConnection")
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SqlLocalDbContext>());
            Database.SetInitializer(new SqlLocalContextIntializer());
        }
        public virtual DbSet<Track> Tracks { get; set; }
    }
}
