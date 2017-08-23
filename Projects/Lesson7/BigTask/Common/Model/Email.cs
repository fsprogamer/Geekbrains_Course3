using System.ComponentModel.DataAnnotations;

namespace Common.Model
{
    public class Email:Entity
    {
        public string Value { get; set; }
        public string Name { get; set; }
    }
}
