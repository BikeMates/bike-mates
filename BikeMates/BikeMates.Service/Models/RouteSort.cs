using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BikeMates.Service.Models
{
    public class RouteSort //TODO: Combine this class with RouteSearch class and use a one class
    {
        [Required]
        public bool ByName { get; set; }

        [Required]
        public bool ByDate { get; set; }

        [Required]
        public bool ByParticipants { get; set; }
       
    }
}