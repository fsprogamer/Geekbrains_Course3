using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameworkApplication
{
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