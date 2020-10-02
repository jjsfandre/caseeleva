using CaseEleva.Models.SearchModel;
using CaseEleva.Models.ViewModel;
using CaseEleva.Repository;
using CaseEleva.Service;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CaseEleva.Controllers
{
    public class TurmaController : Controller
    {

        private readonly TurmaService TurmaService;
        private readonly EscolaRepository EscolaRepository;

        public TurmaController()
        {
            TurmaService = TurmaService.GetInstance();
            EscolaRepository = EscolaRepository.GetInstance();
        }


        // GET: Turma
        public ActionResult Index(TurmaSearchModel searchModel)
        {

            var viewModel = this.TurmaService.GetAssociationViewModel(searchModel);
            ViewBag.Escolas = EscolaRepository.GetAll().ToList();

            return View(viewModel);
        }

        public ActionResult Detail(int? id)
        {

            ViewBag.Escolas = EscolaRepository.GetAll().ToList();
            if (!id.HasValue)
                return View(new TurmaViewModel());

            var viewModel = this.TurmaService.GetById(id.Value);

            return View(viewModel);
        }


        public ActionResult Save(TurmaViewModel formModel)
        {
            this.TurmaService.Save(formModel);

            if (formModel.StatusOperation)
                return RedirectToAction("Index", "Turma");

            ViewBag.Escolas = EscolaRepository.GetAll().ToList();
            ViewBag.ValidationFields = Json(new { formModel.StatusOperation, formModel.FieldsWithError, formModel.StatusMessage });
            return View("Detail",formModel);
        }

        public ActionResult Delete(int[] ids)
        {
            this.TurmaService.Delete(ids);

            return RedirectToAction("Index", "Turma");
        }

    }
}