using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BikeMates.Service.Models
{
    public class RouteSearchParametersViewModel
    {
        //TODO: Remove Required from all properties
        [Required]
        public string Location { get; set; }

        [Required]
        public string DateTo { get; set; }

        [Required]
        public string DateFrom { get; set; }

        [Required]
        public string MinDistance { get; set; }

        [Required]
        public string MaxDistance { get; set; }

        public string OrderByFieldName { get; set; }
    }
}
