using System.ComponentModel.DataAnnotations;

namespace Common.Model
{
    public class Email
    {
        [Key]
        public int EmailId { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }

    }
}
