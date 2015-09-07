using System;

namespace BikeMates.Contracts.Data
{
    public class RouteData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MeetingPlace { get; set; }
        public DateTime Start { get; set; }
        public double Distance { get; set; }
        public bool IsBanned { get; set; }
        public string AuthorName { get; set; }
    }
}
