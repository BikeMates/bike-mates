using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Domain.Entities 
{
    public class Route
    {
        [Key]
        public int Id {get; set;}
        public virtual User User { get; set; } 
    }
}
