using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace BikeMates.Service.Models
{
    public class EditProfileViewModel
    {
        public string Id { get; set; }

        public string Picture { get; set; }

        public string FirstName { get; set; }
        
        public string SecondName { get; set; }

        public string About { get; set; }
       
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)] 
        [Display(Name = "Old Password")]
        public string OldPass { get; set; } //TODO: Use full words like Password. Rename to OldPassword

       
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)] 
        [Display(Name = "New Password")]
        public string NewPass { get; set; } //TODO: Rename to NewPassword

       
        [DataType(DataType.Password)]
        [Display(Name = "Re-type password")]
        [Compare("NewPass", ErrorMessage = "The password and confirmation password do not match.")]
        public string NewPass2 { get; set; } //TODO: Rename to ConfirmationPassword

   


    }
}




