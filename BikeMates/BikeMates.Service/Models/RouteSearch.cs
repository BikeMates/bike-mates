using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BikeMates.Service.Models
{
    public class RouteSearch
    {
        [Required]
        public string Location { get; set; }

        [Required]
        public string Date1 { get; set; }

        [Required]
        public string Date2 { get; set; }

        [Required]
        public string Distance1 { get; set; }

        [Required]
        public string Distance2 { get; set; }
    }
}
