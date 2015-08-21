using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace BikeMates.Service.Models
{
    public class EditProfileViewModel
    {
        [Required]
        public string Picture { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string SecondName { get; set; }

        [Required]
        public string About { get; set; }

       
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)] 
        [Display(Name = "OldPass")]
        public string OldPass { get; set; }

       
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)] 
        [Display(Name = "NewPass")]
        public string NewPass { get; set; }

       
       //[DataType(DataType.Password)]
        [Display(Name = "Re-type password")]
        [Compare("NewPass", ErrorMessage = "The password and confirmation password do not match.")]
        public string NewPass2 { get; set; }

    }
}




