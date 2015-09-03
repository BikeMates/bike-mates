using System;
using System.ComponentModel.DataAnnotations;

namespace BikeMates.Domain.Entities
{
    public enum RouteSortBy //TODO: Rename to RouteSortOptions and move to a separate file
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
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)] //TODO: Remove formating from here
        public DateTime? DateFrom { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]   
        public DateTime? DateTo { get; set; }
        public RouteSortBy SortOrder { get; set; }
    }
}
