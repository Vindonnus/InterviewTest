using FundsLibrary.InterviewTest.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FundsLibrary.InterviewTest.Web.Attributes
{
    public class FundManagerAuthoriseAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {

            var repository = new AccountManagerModelRepository();
            var name = httpContext.User.Identity.Name;
           
            if (!string.IsNullOrEmpty(name))
            {
                var roles = Task.Run(async () => await repository.GetByUsername(name)).Result.Roles;

                foreach (var role in roles)
                {
                    if (Roles.Contains(role.Name))
                        return true;
                }
            }

            return base.AuthorizeCore(httpContext);
        }
    }
}