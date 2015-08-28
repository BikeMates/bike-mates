using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Domain.Entities
{
    public class MapData : Entity
    {
        public Coordinate Start { get; set; }
        public Coordinate End { get; set; }
        public virtual ICollection<Coordinate> Waypoints { get; set; }
    }
}
