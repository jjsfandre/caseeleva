using CaseEleva.Service;
using System.Web.Mvc;

namespace CaseEleva.Controllers
{
    public class ConfiguracaoController : Controller
    {
        private readonly IdiomaService IdiomaService;

        public ConfiguracaoController()
        {
            IdiomaService = IdiomaService.GetInstance();
        }

        public ActionResult AlterarIdioma(int idiomaId)
        {

            IdiomaService.AlterarIdioma(idiomaId);

            return RedirectToAction("Index", "Home");
        }
    }
}