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

        private readonly Func<Escola, EscolaViewModel> selectClauseEscola = escola => new EscolaViewModel
            {
                Id = escola.Id,
                Nome = escola.Nome,
                Logradouro = escola.Logradouro,
                Numero = escola.Numero,
                Complemento = escola.Complemento,
                Cidade = escola.Cidade,
                Telefone = escola.Telefone,
                Diretor = escola.Diretor
            };

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
            

            return Filter(escolas, searchModel).Select(selectClauseEscola).ToList(); 
        }

        public EscolaViewModel GetById(int id)
        {
            var escola = EscolaRepository.GetById(id);            

            return selectClauseEscola(escola);
            
        }

        public void Save(EscolaViewModel formModel)
        {
            if (!this.ValidateSave(formModel))
                return;

            EscolaRepository.Save(formModel);
        }

        public void Delete(int[] ids)
        {
            EscolaRepository.DeleteByIds(ids);
        }

        private bool ValidateSave(EscolaViewModel formModel)
        {
            return true;
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

    }
}