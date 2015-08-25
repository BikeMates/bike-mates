using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BikeMates.Service.Models
{
    public class RouteSort
    {
        [Required]
        public bool ByName { get; set; }

        [Required]
        public bool ByDate { get; set; }

        [Required]
        public bool ByParticipants { get; set; }
       
    }
}