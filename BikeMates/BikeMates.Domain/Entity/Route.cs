using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.DataAccess.Entity
{
    public class Route
    {
        [Key]
        public int Id {get; set;}
        public virtual ApplicationUser User { get; set; } 
    }
}
