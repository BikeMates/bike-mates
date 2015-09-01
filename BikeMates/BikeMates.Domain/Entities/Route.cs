using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Domain.Entities
{
    public class Route : Entity
    {
        public string Title { get; set; }
        public virtual MapData MapData { get; set; }
        public string Description { get; set; }
        public string MeetingPlace { get; set; }
        public DateTime Start { get; set; }
        public double Distance { get; set; }
        public virtual User Author { get; set; }
        public virtual ICollection<User> Subscribers { get; set; }
        public bool IsBanned { get; set; }
    }
}
