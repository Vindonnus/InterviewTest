using FundsLibrary.InterviewTest.Common;
using FundsLibrary.InterviewTest.Service.Controllers;
using FundsLibrary.InterviewTest.Service.Repositories;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace FundsLibrary.InterviewTest.Service.UnitTests.Controllers
{
    [TestFixture]
    public class UserManagerControllerTest
    {
        [Test]
        public async Task ShouldGetByUsername()
        {
            var mock = new Mock<IUserManagerRepository>();
            var controller = new UserManagerController(mock.Object);
            
            var user = new User();
            mock.Setup(m => m.GetByUsername("User1")).Returns(Task.FromResult(user));

            var result = controller.GetByUsername("User1");

            mock.Verify();
            Assert.That(await result, Is.EqualTo(user));
        }

    }
}
