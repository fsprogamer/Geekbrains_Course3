using EntityFrameworkApplication.Model;
using System.Collections.Generic;
using System.Data.Entity;

namespace EntityFrameworkApplication
{
    public class SqlLocalContextIntializer : DropCreateDatabaseIfModelChanges<SqlLocalDbContext>//DropCreateDatabaseAlways<SqlLocalDbContext>
    {
        protected override void Seed(SqlLocalDbContext context)
        {
            context.Tracks.AddRange(new List<Track>
            {
                new Track {  TrackId = 1, ArtistName = "Артист 1",TrackName = "Трек 1" }
            });

            context.SaveChanges();
        }
    }
}