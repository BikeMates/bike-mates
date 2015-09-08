using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BikeMates.Service.Models
{
    public class RouteSearchParametersViewModel
    {
        public string Location { get; set; }
        [Display(Name = "DateTo")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public string DateTo { get; set; }
        [Display(Name = "DateFrom")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public string DateFrom { get; set; }
        public string MinDistance { get; set; }
        public string MaxDistance { get; set; }
        public string OrderByFieldName { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchType { get; set; }
        public string AuthorId { get; set; }
    }
}
