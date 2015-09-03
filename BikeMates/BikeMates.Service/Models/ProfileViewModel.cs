using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BikeMates.Service.Models
{
    public class profileViewModel
    {
      
        [Required]
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string About { get; set; }
        
        public string Picture { get; set; }


     
    }
}