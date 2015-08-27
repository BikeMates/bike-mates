using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Domain.Entities
{
    public class Coordinate : Entity
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
