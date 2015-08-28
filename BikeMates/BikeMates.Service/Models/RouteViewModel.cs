using BikeMates.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BikeMates.Service.Models
{
    public class RouteViewModel
    {
       
        public string Title { get; set; }
        public MapData MapData { get; set; }
        public string Description { get; set; }
        public string MeetingPlace { get; set; }
        public DateTime Start { get; set; }
        public double Distance { get; set; }
        public virtual ICollection<User> Participants { get; set; }
        public bool IsBanned { get; set; }
    }
}