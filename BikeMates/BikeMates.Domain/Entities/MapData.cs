using System.Collections.Generic;

namespace BikeMates.Domain.Entities
{
    public class MapData : Entity
    {
        public virtual Coordinate Start { get; set; }
        public virtual Coordinate End { get; set; }
        public virtual ICollection<Coordinate> Waypoints { get; set; }
    }
}
