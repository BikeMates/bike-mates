using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BikeMates.Service.Models
{
    public class ProfileViewModel
    {
      
        [Required]
        public string id { get; set; } //TODO: Start all properties with Capital letter

        [Required]
        public string firstName { get; set; }

        [Required]
        public string secondName { get; set; }

        [Required]
        public string about { get; set; }


        [Required]
        public string picture { get; set; }


     
    }
}