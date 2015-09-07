using System;
using System.ComponentModel.DataAnnotations;

namespace BikeMates.Domain.Entities
{
    public class RouteSearchParameters
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string MeetingPlace { get; set; }
        public string Description { get; set; }
        public double? MinDistance { get; set; }
        public double? MaxDistance { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateFrom { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateTo { get; set; }
        public RouteSortOptions SortOrder { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
