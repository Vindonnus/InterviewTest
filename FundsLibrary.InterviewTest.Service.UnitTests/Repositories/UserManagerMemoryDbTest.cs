using System;
using FundsLibrary.InterviewTest.Service.Repositories;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FundsLibrary.InterviewTest.Service.UnitTests.Repositories
{
    public class UserManagerMemoryDbTest
    {      
        [Test]
        public async Task ShouldGetByUserName()
        {
            var repo = new UserManagerMemoryDb();
            var result = await repo.GetByUsername("User1");
            Assert.AreEqual("User1", result.Username);
        }
    }
}
