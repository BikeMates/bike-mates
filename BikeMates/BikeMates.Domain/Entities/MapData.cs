using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Domain.Entities
{
    public class MapData
    {
        [Key]
        public int Id { get; set; }
        public Coordinate Start { get; set; }
        public Coordinate End { get; set; }
        public IList<IList<double>> Waypoints { get; set; }
    }
}
