using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using FundsLibrary.InterviewTest.Web.Controllers;
using FundsLibrary.InterviewTest.Web.Models;
using FundsLibrary.InterviewTest.Web.Repositories;
using Moq;
using NUnit.Framework;

namespace FundsLibrary.InterviewTest.Web.UnitTests.Controllers
{
   public class AccountManagerControllerTest
    {
       [Test]
       public async void ShouldGetIndexPage()
       {
           var mock = new Mock<IAccountManagerModelRepository>();
           var loginModel = new LoginModel(){Username="User1"};
           mock.Setup(m => m.GetByUsername("User1")).Returns(Task.FromResult(loginModel));
           var controller = new AccountController(mock.Object);
          // var result = await controller.Index(loginModel);
          // Assert.That(result, Is.TypeOf<ViewResult>());
          // mock.Verify();
          //Assert.That(((ViewResult)result).Model, Is.EqualTo(loginModel));
       }
    }
}
