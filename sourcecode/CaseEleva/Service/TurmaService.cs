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
    public class TurmaService
    {

        private static TurmaService _instance;
        private readonly TurmaRepository TurmaRepository;

        private readonly Func<Turma, TurmaViewModel> selectClauseTurma = Turma => new TurmaViewModel
        {
                Id = Turma.Id,
                EscolaId = Turma.EscolaId,
                Escola = Turma.Escola,
                Codigo = Turma.Codigo,
                Professor = Turma.Professor,
                TotalAlunos = Turma.Aluno.Count,
                Descricao = Turma.Descricao
        };

        private TurmaService() 
        {
            TurmaRepository = TurmaRepository.GetInstance();
        }

        public static TurmaService GetInstance()
        {
            if (_instance == null)
                _instance = new TurmaService();

            return _instance;
        }

        public AssociationViewModel<TurmaSearchModel, TurmaViewModel> GetAssociationViewModel(TurmaSearchModel searchModel)
        {
            var Turmas = Search(searchModel);

            return new AssociationViewModel<TurmaSearchModel, TurmaViewModel>
            {
                Objects = Turmas,
                TotalCount = Turmas.Count,
                SearchModel = searchModel
            };
        }

        public List<TurmaViewModel> Search(TurmaSearchModel searchModel)
        {
            var Turmas = TurmaRepository.GetAll();
            

            return Filter(Turmas, searchModel).Select(selectClauseTurma).ToList(); 
        }

        public TurmaViewModel GetById(int id)
        {
            var Turma = TurmaRepository.GetById(id);            

            return selectClauseTurma(Turma);
            
        }

        public void Save(TurmaViewModel formModel)
        {
            if (!this.ValidateSave(formModel))
                return;

            TurmaRepository.Save(formModel);
        }

        public void Delete(int[] ids)
        {
            TurmaRepository.DeleteByIds(ids);
        }

        private bool ValidateSave(TurmaViewModel formModel)
        {
            formModel.StatusOperation = true;
            formModel.FieldsWithError.Clear();
            var messageError =  new List<String>();

            if (String.IsNullOrEmpty(formModel.Codigo))
            {
                messageError.Add(String.Format(GlobalizationController.GetInstance().GetString("The field '{0}' is required."), GlobalizationController.GetInstance().GetString("Code")));
                formModel.FieldsWithError.Add(nameof(formModel.Codigo));
            }
            if (String.IsNullOrEmpty(formModel.Professor))
            {
                messageError.Add(String.Format(GlobalizationController.GetInstance().GetString("The field '{0}' is required."), GlobalizationController.GetInstance().GetString("Teacher")));
                formModel.FieldsWithError.Add(nameof(formModel.Professor));
            }
            if (!formModel.EscolaId.HasValue)
            {
                messageError.Add(String.Format(GlobalizationController.GetInstance().GetString("The field '{0}' is required."), GlobalizationController.GetInstance().GetString("School")));
                formModel.FieldsWithError.Add(nameof(formModel.EscolaId));
            }

            if (messageError.Count > 0)
            {
                formModel.StatusMessage = string.Join("<br>",messageError);
                formModel.StatusOperation = false;

            }

            return formModel.StatusOperation;
        }
        private IQueryable<Turma> Filter(IQueryable<Turma> baseQuery, TurmaSearchModel searchModel)
        {

            if (!string.IsNullOrEmpty(searchModel.Codigo))
                baseQuery = baseQuery.Where(x => x.Codigo.Contains(searchModel.Codigo));

            if (!string.IsNullOrEmpty(searchModel.Professor))
                baseQuery = baseQuery.Where(x => x.Professor.Contains(searchModel.Professor));

            if (searchModel.EscolaId.HasValue)
                baseQuery = baseQuery.Where(x => x.EscolaId==searchModel.EscolaId);

            return baseQuery;
        }

    }
}