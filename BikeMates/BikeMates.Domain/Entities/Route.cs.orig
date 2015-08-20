using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Domain.Entities
{
    public class Route //TODO: Please create a base class for Entity with Id property
    {
        [Key]
        //public int Id { get; set; }
       // public virtual User User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MeetingPlace { get; set; }
        public DateTime Start { get; set; }

        //TODO: Remove comments
        //Do we need this field? 
        //public DateTime End { get; set; }
        public int ParticipantsCount { get; set; }  //TODO: Remove this field, we will calculate this in the query
        /// <summary>
        /// Total route length in meters
        /// </summary>
        public int Distance { get; set; } //TODO: Use double type, probably we will have a decimal part
       // public bool IsBanned { get; set; }

    }
}
