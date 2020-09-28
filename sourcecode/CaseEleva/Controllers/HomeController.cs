using CaseEleva.Models;
using System.Linq;
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
            using (var context = new CaseElevaEntities())
            {
                ViewBag.Idiomas = context.Idioma.ToList();
            }

            return View();
        }

    }
}