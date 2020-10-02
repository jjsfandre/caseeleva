using CaseEleva.Helpers;
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
                Diretor = escola.Diretor,
                Estado = escola.Estado
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
            formModel.StatusOperation = true;
            formModel.FieldsWithError.Clear();
            var messageError =  new List<String>();

            if (String.IsNullOrEmpty(formModel.Nome))
            {
                messageError.Add(String.Format(GlobalizationController.GetInstance().GetString("The field '{0}' is required."), GlobalizationController.GetInstance().GetString("Name")));
                formModel.FieldsWithError.Add(nameof(formModel.Nome));
            }
            if (String.IsNullOrEmpty(formModel.Logradouro))
            {
                messageError.Add(String.Format(GlobalizationController.GetInstance().GetString("The field '{0}' is required."), GlobalizationController.GetInstance().GetString("Public place")));
                formModel.FieldsWithError.Add(nameof(formModel.Logradouro));
            }
            if (String.IsNullOrEmpty(formModel.Numero))
            {
                messageError.Add(String.Format(GlobalizationController.GetInstance().GetString("The field '{0}' is required."), GlobalizationController.GetInstance().GetString("Number")));
                formModel.FieldsWithError.Add(nameof(formModel.Numero));
            }
            if (String.IsNullOrEmpty(formModel.Cidade))
            {
                messageError.Add(String.Format(GlobalizationController.GetInstance().GetString("The field '{0}' is required."), GlobalizationController.GetInstance().GetString("City")));
                formModel.FieldsWithError.Add(nameof(formModel.Cidade));
            }
            if (String.IsNullOrEmpty(formModel.Estado))
            {
                messageError.Add(String.Format(GlobalizationController.GetInstance().GetString("The field '{0}' is required."), GlobalizationController.GetInstance().GetString("State")));
                formModel.FieldsWithError.Add(nameof(formModel.Estado));
            }

            if (messageError.Count > 0)
            {
                formModel.StatusMessage = string.Join("<br>",messageError);
                formModel.StatusOperation = false;

            }

            return formModel.StatusOperation;
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