using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BikeMates.Service.Models
{
    public class ValidationResponseViewModel
    {

        public ValidationResponseViewModel(List<string> passwordErrors , List<string> informationStatus ,List<string> nameErrors)
            {
                this.passwordErrors = passwordErrors;
                this.informationStatus = informationStatus;
                this.nameErrors = nameErrors;
            }

        public List<string> passwordErrors;
        public List<string> informationStatus;
        public List<string> nameErrors;
    }
}