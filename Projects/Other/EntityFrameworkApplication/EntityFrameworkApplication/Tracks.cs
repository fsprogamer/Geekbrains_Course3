using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkApplication
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
