using CaseEleva.Models.SearchModel;
using CaseEleva.Models.ViewModel;
using CaseEleva.Service;
using System;
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

            var viewModel = this.EscolaService.GetAssociationViewModel(searchModel);

            return View(viewModel);
        }

        public ActionResult Detail(int? id)
        {
            if (!id.HasValue)
                return View();

            var viewModel = this.EscolaService.GetById(id.Value);

            return View(viewModel);
        }


        public ActionResult Save(EscolaViewModel formModel)
        {
            this.EscolaService.Save(formModel);
            if (formModel.StatusOperation)
                return RedirectToAction("Index", "Escola");

            return View("Index");
        }

        public ActionResult Delete(int[] ids)
        {
            this.EscolaService.Delete(ids);

            return RedirectToAction("Index", "Escola");
        }

    }
}