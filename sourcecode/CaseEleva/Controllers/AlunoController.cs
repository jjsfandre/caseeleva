using CaseEleva.Models.SearchModel;
using CaseEleva.Models.ViewModel;
using CaseEleva.Repository;
using CaseEleva.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CaseEleva.Controllers
{
    public class AlunoController : Controller
    {

        private readonly AlunoService AlunoService;
        private readonly TurmaRepository TurmaRepository;
        private readonly EscolaRepository EscolaRepository;

        public AlunoController()
        {
            AlunoService = AlunoService.GetInstance();
            TurmaRepository = TurmaRepository.GetInstance();
            EscolaRepository = EscolaRepository.GetInstance();
        }


        // GET: Aluno
        public ActionResult Index(AlunoSearchModel searchModel)
        {

            var viewModel = this.AlunoService.GetAssociationViewModel(searchModel);
            ViewBag.Escolas = EscolaRepository.GetAll().ToList();
            if (searchModel.TurmaId.HasValue)
                viewModel.SearchModel.EscolaId = TurmaRepository.GetById(searchModel.TurmaId.Value).EscolaId;
            if (viewModel.SearchModel.EscolaId.HasValue)
                ViewBag.Turmas = TurmaRepository.GetByEscolaIds(new int[] { viewModel.SearchModel.EscolaId.Value }).ToList();
            else
                ViewBag.Turmas = Enumerable.Empty<SelectListItem>();

            return View(viewModel);
        }

        public ActionResult Detail(int? id)
        {

            ViewBag.Escolas = EscolaRepository.GetAll().ToList();
            ViewBag.Turmas = Enumerable.Empty<SelectListItem>();
            if (!id.HasValue)
                return View(new AlunoViewModel());
                
            var viewModel = this.AlunoService.GetById(id.Value);
            ViewBag.Turmas = TurmaRepository.GetByEscolaIds(new int[] { viewModel.Turma.EscolaId }).ToList();

            return View(viewModel);
        }


        public ActionResult Save(AlunoViewModel formModel)
        {
            this.AlunoService.Save(formModel);

            if (formModel.StatusOperation)
                return RedirectToAction("Index", "Aluno");

            ViewBag.Escolas = EscolaRepository.GetAll().ToList();
            ViewBag.ValidationFields = Json(new { formModel.StatusOperation, formModel.FieldsWithError, formModel.StatusMessage });
            return View("Detail", formModel);
        }

        public ActionResult Delete(int[] ids)
        {
            this.AlunoService.Delete(ids);

            return RedirectToAction("Index", "Aluno");
        }

        public JsonResult GetTurmasByEscolaId(int? EscolaId)
        {
            var turmas = new List<TurmaViewModel>();
            if (!EscolaId.HasValue)
                return Json(new { Success = true, Turmas = turmas }, JsonRequestBehavior.AllowGet);

            int[] ids = { EscolaId.Value };

            turmas = TurmaRepository.GetByEscolaIds(ids).Select(escola => new TurmaViewModel {
                Id = escola.Id,
                Codigo =  escola.Codigo
            }).ToList();

            return Json(new { Success = true, Turmas = turmas },JsonRequestBehavior.AllowGet);
        }

    }
}