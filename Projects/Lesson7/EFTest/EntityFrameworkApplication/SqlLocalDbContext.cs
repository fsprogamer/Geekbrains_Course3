﻿using System.Data.Entity;
using EntityFrameworkApplication.Model;

namespace EntityFrameworkApplication
{
    public class SqlLocalDbContext : DbContext
    {
        public SqlLocalDbContext() : base("SqlLocalDatabaseConnection")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SqlLocalDbContext>());
        }
        public virtual DbSet<Track> Tracks { get; set; }
    }
}
