﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BikeMates.Service.Models
{
    public class LoginResponseModel
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public string ExpiresIn { get; set; }
    }
}