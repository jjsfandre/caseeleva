using System.Web.Mvc;

namespace CaseEleva.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            return View("Index");
        }
    }
}