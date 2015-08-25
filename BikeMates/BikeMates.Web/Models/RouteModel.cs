using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BikeMates.Domain.Entities;

namespace BikeMates.Web.Models
{
    public class Route //TODO: Move to the Service project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        public MapData MapData { get; set; }

        [Required]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Meeting place")]
        [DataType(DataType.MultilineText)]
        public string MeetingPlace { get; set; }

        [Required]
        [Display(Name = "Start date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Start { get; set; }

        public double Distance { get; set; }

        public List<User> Participants { get; set; }

        public bool IsBanned { get; set; }

        public BikeMates.Domain.Entities.Route MapToDomain()
        {
            BikeMates.Domain.Entities.Route _route = new BikeMates.Domain.Entities.Route
            {
                Id = this.Id,
                Title = this.Title,
                MapData = this.MapData,
                Description = this.Description,
                MeetingPlace = this.MeetingPlace,
                Start = this.Start,
                Distance = this.Distance,
                Participants = this.Participants,
                IsBanned = this.IsBanned
            };
            return _route;
        }
        public BikeMates.Domain.Entities.Route MapToDomain(BikeMates.Web.Models.Route route)
        {
            BikeMates.Domain.Entities.Route _route = new BikeMates.Domain.Entities.Route
            {
                Id = route.Id,
                Title = route.Title,
                MapData = route.MapData,
                Description = route.Description,
                MeetingPlace = route.MeetingPlace,
                Start = route.Start,
                Distance = route.Distance,
                Participants = route.Participants,
                IsBanned = route.IsBanned
            };
            return _route;
        }
    }
}