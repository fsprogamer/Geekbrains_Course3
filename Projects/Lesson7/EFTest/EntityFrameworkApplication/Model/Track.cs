using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkApplication.Model
{
    [Table("Tracks")]
    public class Track
    {
        [Key]
        public int TrackId { get; set; }
        [Required]
        [Column("ArtistName")]
        public string ArtistName { get; set; }
        [Required]
        [Column("TrackName")]
        public string TrackName { get; set; }
    }
}
