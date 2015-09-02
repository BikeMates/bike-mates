using System;
using System.Collections.Generic;

namespace BikeMates.Domain.Entities
{
    public class Route : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string MeetingPlace { get; set; }
        public DateTime Start { get; set; }
        public double Distance { get; set; }
        public bool IsBanned { get; set; }
        public virtual MapData MapData { get; set; }
        public virtual User Author { get; set; }
        public virtual ICollection<User> Subscribers { get; set; }
    }
}
