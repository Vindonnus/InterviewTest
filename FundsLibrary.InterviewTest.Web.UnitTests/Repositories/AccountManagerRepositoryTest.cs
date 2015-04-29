using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundsLibrary.InterviewTest.Common;
using FundsLibrary.InterviewTest.Web.Models;
using FundsLibrary.InterviewTest.Web.Models.Mappers;
using FundsLibrary.InterviewTest.Web.Repositories;
using Moq;
using NUnit.Framework;

namespace FundsLibrary.InterviewTest.Web.UnitTests.Repositories
{
   public class AccountManagerRepositoryTest
    {
        [Test]
        public async void ShouldGet()
        {
            var mockServiceClient = new Mock<IHttpClientWrapper>();
            var mockToAccountManagerModelMapper = new Mock<IMapper<User, LoginModel>>();
            var user = new User(){Username = "User1"};
  
            mockServiceClient
                .Setup(m => m.GetAndReadFromContentGetAsync<User>("api/Account/" + user.Username))
                .Returns(Task.FromResult(user));
            var loginModel = new LoginModel();
            mockToAccountManagerModelMapper
                .Setup(m => m.Map(It.IsAny<User>()))
                .Returns(loginModel);
            var repository = new AccountManagerModelRepository(
               mockServiceClient.Object,
               mockToAccountManagerModelMapper.Object);

            var result = await repository.GetByUsername(user.Username);

            mockToAccountManagerModelMapper.Verify();
            mockServiceClient.Verify();
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(loginModel));
        }

    }
}
