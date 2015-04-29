using FundsLibrary.InterviewTest.Common;
using FundsLibrary.InterviewTest.Web.Models;
using FundsLibrary.InterviewTest.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FundsLibrary.InterviewTest.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountManagerModelRepository _repository;

        // ReSharper disable once UnusedMember.Global
        public AccountController()
            : this(new AccountManagerModelRepository())
        {}

        public AccountController(IAccountManagerModelRepository repository)
        {
            _repository = repository;
        }

        // Post: Get login by username
        [HttpPost]
         public async Task<ActionResult> Index(LoginModel loginModel)
         {
            LoginModel user = null;

            try
            {
                user = await _repository.GetByUsername(loginModel.Username);
            }
            catch (Exception)
            {
                ModelState.AddModelError("LoginError", "No such user");
            }

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Username, false);
 
                this.Session["IsAdmin"] = user.Roles.Any(x => x.Name == "Admin");

                if (loginModel.Password != user.Password)
                {
                    ModelState.AddModelError("LoginError", "Invalid password");
                }

                return RedirectToAction("Index", "FundManager");
            }

            return View();                          

        }

         //Get: login page
         public async Task<ActionResult> Index()
         {      
             return View();
         }

         //Get: logout page
         public async Task<ActionResult> LogOff()
         {
             FormsAuthentication.SignOut();

             return RedirectToAction("Index");
         }
       
    }
}