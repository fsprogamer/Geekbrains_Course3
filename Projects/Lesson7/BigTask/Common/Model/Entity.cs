using System.ComponentModel.DataAnnotations;

namespace Common.Model
{
    public class Entity : IEntity
    {
        [Key]
        public int Id { get; set; }        
    }
}
