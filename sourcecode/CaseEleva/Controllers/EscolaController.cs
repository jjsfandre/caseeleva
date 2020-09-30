﻿using CaseEleva.Models.SearchModel;
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

            return PartialView(viewModel);
        }

        public ActionResult Detail(int? id)
        {
            if (!id.HasValue)
                throw new ArgumentNullException(nameof(id));

            var viewModel = this.EscolaService.GetById(id.Value);

            return PartialView(viewModel);
        }


        public ActionResult Save(EscolaViewModel formModel)
        {
            this.EscolaService.Save(formModel);
            if (formModel.StatusOperation)
                return RedirectToAction("Index", "Escola");

            return PartialView("Index");
        }

    }
}