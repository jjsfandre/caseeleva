using CaseEleva.Models;
using System.Linq;
using System.Web.Mvc;

namespace CaseEleva.Controllers
{
    public class ConfiguracaoController : Controller
    {

        public ActionResult AlterarIdioma(int idiomaId)
        {
            using (var context = new CaseElevaEntities())
            {
                context.Configuracao.SingleOrDefault().IdiomaId = idiomaId;
                context.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}