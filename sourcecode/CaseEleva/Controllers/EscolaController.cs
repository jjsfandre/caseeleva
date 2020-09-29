using CaseEleva.Models.SearchModel;
using CaseEleva.Service;
using System.Web.Mvc;

namespace CaseEleva.Controllers
{
    public class EscolaController : Controller
    {

        private readonly EscolaService EscolaService;

        public EscolaController()
        {
            EscolaService = EscolaService.GetInstance();
        }


        // GET: Escola
        public ActionResult Index(EscolaSearchModel searchModel)
        {

            var viewModel = this.EscolaService.Search(searchModel);

            return View(viewModel);
        }
    }
}