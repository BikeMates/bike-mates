using BikeMates.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace BikeMates.Domain.Entities
{
    public enum RouteSortBy
    {  
        Date, Title, Subscribers
    }

    public class RouteSearchParameters
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string MeetingPlace { get; set; }
        public string Description { get; set; }
        public double? MinDistance { get; set; }
        public double? MaxDistance { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateFrom { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]   
        public DateTime? DateTo { get; set; }
        public RouteSortBy SortOrder { get; set; }
    }
}
