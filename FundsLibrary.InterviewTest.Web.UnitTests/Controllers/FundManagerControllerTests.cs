using System;
using System.CodeDom;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using FundsLibrary.InterviewTest.Common;
using FundsLibrary.InterviewTest.Web.Controllers;
using FundsLibrary.InterviewTest.Web.Models;
using FundsLibrary.InterviewTest.Web.Repositories;
using Moq;
using NUnit.Framework;

namespace FundsLibrary.InterviewTest.Web.UnitTests.Controllers
{
    public class FundManagerControllerTests
    {
        [Test]
        public async void ShouldGetIndexPage()
        {
            var mock = new Mock<IFundManagerModelRepository>();
            var fundManagerModels = new FundManagerModel[0].AsEnumerable();
            mock.Setup(m => m.GetAll()).Returns(Task.FromResult(fundManagerModels));
            var controller = new FundManagerController(mock.Object);

            var result = await controller.Index(1, string.Empty, null);

            Assert.That(result, Is.TypeOf<ViewResult>());
            mock.Verify();
            Assert.That(((ViewResult)result).Model, Is.EqualTo(fundManagerModels));
        }

        [Test]
        public async void ShouldGetDetailsPage()
        {
            var guid = Guid.NewGuid();
            var mock = new Mock<IFundManagerModelRepository>();
            var fundManagerModel = new FundManagerModel();
            mock.Setup(m => m.Get(guid)).Returns(Task.FromResult(fundManagerModel));
            var controller = new FundManagerController(mock.Object);

            var result = await controller.Details(guid);

            Assert.That(result, Is.TypeOf<ViewResult>());
            mock.Verify();
            Assert.That(((ViewResult)result).Model, Is.EqualTo(fundManagerModel));
        }

        [Test]
        public async void ShouldCreateFundManage()
        {
            var mock = new Mock<IFundManagerModelRepository>();
            var fm = new FundManagerModel()
            {
                Biography = "Some",
                Location = Location.London,
                ManagedSince = DateTime.Now,
                Name = "Fred"
            };

            mock.Setup(m => m.Create(fm)).Returns(Task.FromResult(fm));
            var controller = new FundManagerController(mock.Object);
            var result = await controller.Create(fm);

            Assert.That(result, Is.TypeOf<RedirectToRouteResult>());
            mock.Verify();
        }

        [Test]
        public async void ShouldEditFundManage()
        {
            var guid = Guid.NewGuid();
            var mock = new Mock<IFundManagerModelRepository>();
            var fm = new FundManagerModel()
            {
                Id = guid,
                Biography = "Some1",
                Location = Location.London,
                ManagedSince = DateTime.Now,
                Name = "Fred1"
            };

            mock.Setup(m => m.Update(fm)).Returns(Task.FromResult(fm));
            var controller = new FundManagerController(mock.Object);
            var result = await controller.Edit(fm);

            Assert.That(result, Is.TypeOf<ViewResult>());
            mock.Verify();
            Assert.That(((ViewResult)result).Model, Is.EqualTo(fm));
        }
    }
}
