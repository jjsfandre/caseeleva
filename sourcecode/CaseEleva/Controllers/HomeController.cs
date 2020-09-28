using System.Web.Mvc;

namespace CaseEleva.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

    }
}