using CaseEleva.Service;
using System.Web.Mvc;

namespace CaseEleva.Controllers
{
    public class IdiomaController : Controller
    {

        private readonly IdiomaService IdiomaService;

        public IdiomaController()
        {
            IdiomaService = IdiomaService.GetInstance();
        }

        // GET: Idioma
        public ActionResult GetIdiomas()
        {

            ViewBag.Idiomas = IdiomaService.GetAll();

            return PartialView();
        }
    }
}