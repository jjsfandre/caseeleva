using CaseEleva.Models;
using CaseEleva.Models.SearchModel;
using CaseEleva.Models.ViewModel;
using CaseEleva.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CaseEleva.Service
{
    public class EscolaService
    {

        private static EscolaService _instance;
        private readonly EscolaRepository EscolaRepository;

        private EscolaService() 
        {
            EscolaRepository = EscolaRepository.GetInstance();
        }

        public static EscolaService GetInstance()
        {
            if (_instance == null)
                _instance = new EscolaService();

            return _instance;
        }

        public AssociationViewModel<EscolaSearchModel,EscolaViewModel> GetAssociationViewModel(EscolaSearchModel searchModel)
        {
            var escolas = Search(searchModel);

            return new AssociationViewModel<EscolaSearchModel, EscolaViewModel>
            {
                Objects = escolas,
                TotalCount = escolas.Count,
                SearchModel = searchModel
            };
        }

        public List<EscolaViewModel> Search(EscolaSearchModel searchModel)
        {
            var escolas = EscolaRepository.GetAll();
            

            return Filter(escolas, searchModel).Select(x => new EscolaViewModel
            {
                Id = x.Id,
                Nome = x.Nome,
                Logradouro = x.Logradouro,
                Numero = x.Numero,
                Complemento = x.Complemento,
                Cidade = x.Cidade,
                Telefone = x.Telefone,
                Diretor = x.Diretor
            }).ToList();
        }

        private IQueryable<Escola> Filter(IQueryable<Escola> baseQuery, EscolaSearchModel searchModel)
        {

            if (!string.IsNullOrEmpty(searchModel.Nome))
                baseQuery = baseQuery.Where(x => x.Nome.Contains(searchModel.Nome));

            if (!string.IsNullOrEmpty(searchModel.Cidade))
                baseQuery = baseQuery.Where(x => x.Cidade.Contains(searchModel.Cidade));

            if (!string.IsNullOrEmpty(searchModel.Diretor))
                baseQuery = baseQuery.Where(x => x.Diretor.Contains(searchModel.Diretor));

            return baseQuery;
        }

        public void Save(EscolaViewModel formModel)
        {
            if (!this.ValidateSave(formModel))
                return;

            EscolaRepository.Save(formModel);
        }

        private bool ValidateSave(EscolaViewModel formModel)
        {
            return true;
        }

    }
}