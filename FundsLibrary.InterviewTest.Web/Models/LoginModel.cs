using FundsLibrary.InterviewTest.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FundsLibrary.InterviewTest.Web.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please supply your username", AllowEmptyStrings = false)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please supply your password", AllowEmptyStrings = false)]
        public string Password { get; set; }

        public IList<Role> Roles { get; set; }
    }
}