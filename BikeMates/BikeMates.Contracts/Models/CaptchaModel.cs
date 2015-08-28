using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BikeMates.Contracts.Models
{
    public class CaptchaModel //TODO: Remove as it is never used
    {
        public bool Success { get; set; }
        public List<string> ErrorCodes { get; set; }
    }
}