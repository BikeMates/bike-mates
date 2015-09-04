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
        [Display(Name = "DateTo")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public string DateTo { get; set; }

        [Required]
        [Display(Name = "DateFrom")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public string DateFrom { get; set; }

        [Required]
        public string MinDistance { get; set; }

        [Required]
        public string MaxDistance { get; set; }

        public string OrderByFieldName { get; set; }
    }
}
