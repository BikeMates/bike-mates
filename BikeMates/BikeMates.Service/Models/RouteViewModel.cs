using BikeMates.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace BikeMates.Service.Models
{
    public class RouteViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        public string MapData { get; set; }

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
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Start { get; set; }

        public string Distance { get; set; }

        public string Subscribers { get; set; }
        public string Author { get; set; }
        public bool IsSubscribed { get; set; }

        public bool IsBanned { get; set; }

        public Route MapToDomain()
        {
            //TODO: Use Automapper for mapping
            Route _route = new Route
            {
                Id = this.Id,
                Title = this.Title,
                MapData = JsonConvert.DeserializeObject<MapData>(this.MapData),
                Description = this.Description,
                MeetingPlace = this.MeetingPlace,
                Start = this.Start,
                Distance = Double.Parse(this.Distance, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo),
                //Author = JsonConvert.DeserializeObject<User>(this.Author),
                //Subscribers = JsonConvert.DeserializeObject<List<User>>(this.Subscribers),
                IsBanned = this.IsBanned
            };
            return _route;
        }
        public static Route MapToDomain(RouteViewModel route)
        {
            Route _route = new Route
            {
                Id = route.Id,
                Title = route.Title,
                MapData = JsonConvert.DeserializeObject<MapData>(route.MapData),
                Description = route.Description,
                MeetingPlace = route.MeetingPlace,
                Start = route.Start,
                Distance = Double.Parse(route.Distance, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo),
                //Author = JsonConvert.DeserializeObject<User>(route.Author),
                //Subscribers = JsonConvert.DeserializeObject<List<User>>(route.Subscribers),
                IsBanned = route.IsBanned
            };
            return _route;
        }
        public static RouteViewModel MapToViewModel(Route route)
        {
            //yes, it's dirty code. it's temporary
            var subscribers = new List<SubscriberViewModel>();
            foreach (var item in route.Subscribers)
            {
                subscribers.Add(new SubscriberViewModel
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    SecondName = item.SecondName
                });
            }

            RouteViewModel _route = new RouteViewModel
            {
                Id = route.Id,
                Title = route.Title,
                MapData = JsonConvert.SerializeObject(route.MapData),
                Description = route.Description,
                MeetingPlace = route.MeetingPlace,
                Start = route.Start,
                Distance = route.Distance.ToString(),
                Author = JsonConvert.SerializeObject(new SubscriberViewModel
                {
                    Id = route.Author.Id,
                    FirstName = route.Author.FirstName,
                    SecondName = route.Author.SecondName
                }),
                Subscribers = JsonConvert.SerializeObject(subscribers),
                IsBanned = route.IsBanned
            };
            return _route;
        }

    }
}