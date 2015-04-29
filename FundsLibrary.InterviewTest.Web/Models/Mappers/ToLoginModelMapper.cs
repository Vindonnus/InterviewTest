using FundsLibrary.InterviewTest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FundsLibrary.InterviewTest.Web.Models.Mappers
{
    public class ToLoginModelMapper : IMapper<User, LoginModel>
    {
        public LoginModel Map(User obj)
        {
            return new LoginModel
            {
                Username = obj.Username,
                Password = obj.Password,
                Roles = obj.Roles
            };
        }


        public User ReverseMap(LoginModel obj)
        {
            throw new NotImplementedException();
        }
    }
}