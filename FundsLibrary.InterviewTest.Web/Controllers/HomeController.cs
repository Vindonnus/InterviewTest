using System.Web.Mvc;

namespace FundsLibrary.InterviewTest.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Account");
        }
    }
}
