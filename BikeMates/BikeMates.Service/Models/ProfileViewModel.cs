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

        public string firstName { get; set; }

        public string secondName { get; set; }

        public string about { get; set; }
        
        public string picture { get; set; }


     
    }
}