using CaseEleva.Models;
using CaseEleva.Service;
using System.Linq;
using System.Web.Mvc;

namespace CaseEleva.Controllers
{
    public class HomeController : Controller
    {

        private readonly IdiomaService IdiomaService;

        public HomeController()
        {
            IdiomaService = IdiomaService.GetInstance();
        }


        // GET: Home
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Idiomas  = IdiomaService.GetAll();

            return View();
        }

    }
}