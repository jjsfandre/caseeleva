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
    public class AlunoService
    {

        private static AlunoService _instance;
        private readonly AlunoRepository AlunoRepository;

        private readonly Func<Aluno, AlunoViewModel> selectClauseAluno = Aluno => new AlunoViewModel
        {
                Id = Aluno.Id,
                TurmaId = Aluno.TurmaId,
                Turma = Aluno.Turma,
                Nome = Aluno.Nome,
                Email = Aluno.Email,
                Telefone = Aluno.Telefone
        };

        private AlunoService() 
        {
            AlunoRepository = AlunoRepository.GetInstance();
        }

        public static AlunoService GetInstance()
        {
            if (_instance == null)
                _instance = new AlunoService();

            return _instance;
        }

        public AssociationViewModel<AlunoSearchModel, AlunoViewModel> GetAssociationViewModel(AlunoSearchModel searchModel)
        {
            var Alunos = Search(searchModel);

            return new AssociationViewModel<AlunoSearchModel, AlunoViewModel>
            {
                Objects = Alunos,
                TotalCount = Alunos.Count,
                SearchModel = searchModel
            };
        }

        public List<AlunoViewModel> Search(AlunoSearchModel searchModel)
        {
            var Alunos = AlunoRepository.GetAll();
            

            return Filter(Alunos, searchModel).Select(selectClauseAluno).ToList(); 
        }

        public AlunoViewModel GetById(int id)
        {
            var Aluno = AlunoRepository.GetById(id);            

            return selectClauseAluno(Aluno);
            
        }

        public void Save(AlunoViewModel formModel)
        {
            if (!this.ValidateSave(formModel))
                return;

            AlunoRepository.Save(formModel);
        }

        public void Delete(int[] ids)
        {
            AlunoRepository.DeleteByIds(ids);
        }

        private bool ValidateSave(AlunoViewModel formModel)
        {
            formModel.StatusOperation = true;
            formModel.FieldsWithError.Clear();
            var messageError =  new List<String>();

            if (String.IsNullOrEmpty(formModel.Nome))
            {
                messageError.Add(String.Format(GlobalizationController.GetInstance().GetString("The field '{0}' is required."), GlobalizationController.GetInstance().GetString("Name")));
                formModel.FieldsWithError.Add(nameof(formModel.Nome));
            }
            if (!formModel.TurmaId.HasValue)
            {
                messageError.Add(String.Format(GlobalizationController.GetInstance().GetString("The field '{0}' is required."), GlobalizationController.GetInstance().GetString("Class")));
                formModel.FieldsWithError.Add(nameof(formModel.TurmaId));
            }

            if (messageError.Count > 0)
            {
                formModel.StatusMessage = string.Join("<br>",messageError);
                formModel.StatusOperation = false;

            }

            return formModel.StatusOperation;
        }
        private IQueryable<Aluno> Filter(IQueryable<Aluno> baseQuery, AlunoSearchModel searchModel)
        {

            if (!string.IsNullOrEmpty(searchModel.Nome))
                baseQuery = baseQuery.Where(x => x.Nome.Contains(searchModel.Nome));

            if (!string.IsNullOrEmpty(searchModel.Email))
                baseQuery = baseQuery.Where(x => x.Email.Contains(searchModel.Email));

            if (searchModel.TurmaId.HasValue)
                baseQuery = baseQuery.Where(x => x.TurmaId == searchModel.TurmaId);

            return baseQuery;
        }

    }
}