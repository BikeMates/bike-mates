using System.ComponentModel.DataAnnotations;

namespace BikeMates.Domain.Entities
{
    public class Entity: IEntity<int>
    {
        [Key]
        public int Id { get; set; }
    }
}
