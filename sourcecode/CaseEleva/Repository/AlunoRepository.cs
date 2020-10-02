using CaseEleva.Models;
using CaseEleva.Models.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace CaseEleva.Repository
{
    public class AlunoRepository
    {

        private static AlunoRepository _instance;

        private AlunoRepository() { }

        public static AlunoRepository GetInstance()
        {
            if (_instance == null)
                _instance = new AlunoRepository();

            return _instance;
        }

        public IQueryable<Aluno> GetAll()
        {
            var context = DBFactory.GetInstance().GetDb();
            return context.Aluno.AsQueryable();
        }

        public void Save(AlunoViewModel formModel)
        {
            var context = DBFactory.GetInstance().GetDb();
            Aluno Aluno;
            if (formModel.Id.HasValue)
            {
                Aluno = GetById(formModel.Id.Value, context);
            }
            else
            {
                Aluno = new Aluno();
                context.Aluno.Add(Aluno);
            }
            Aluno.TurmaId = formModel.TurmaId.Value;
            Aluno.Nome = formModel.Nome;
            Aluno.Email = formModel.Email;
            Aluno.Telefone = formModel.Telefone;
            context.SaveChanges();
        }

        public Aluno GetById(int id, CaseElevaEntities context = null)
        {
            if (context == null)
                context = DBFactory.GetInstance().GetDb();

            return context.Aluno.FirstOrDefault(x => x.Id == id);
        }

        public List<Aluno> GetByIds(int[] ids, CaseElevaEntities context = null)
        {
            if (context == null)
                context = DBFactory.GetInstance().GetDb();

            return context.Aluno.Where(x => ids.Contains(x.Id)).ToList();
        }

        public List<Aluno> GetByTurmaIds(int[] ids, CaseElevaEntities context = null)
        {
            if (context == null)
                context = DBFactory.GetInstance().GetDb();

            return context.Aluno.Where(x => ids.Contains(x.TurmaId)).ToList();
        }

        public void DeleteByIds(int[] ids)
        {
            var context = DBFactory.GetInstance().GetDb();
            List<Aluno> Alunos = GetByIds(ids, context);
            context.Aluno.RemoveRange(Alunos);
            context.SaveChanges();

        }

        public void DeleteByTurmaIds(int[] ids, CaseElevaEntities context = null, bool isToSaveContext = true)
        {
            if (context == null)
                context = DBFactory.GetInstance().GetDb();

            List<Aluno> Alunos = GetByTurmaIds(ids, context);
            context.Aluno.RemoveRange(Alunos);

            if (isToSaveContext)
                context.SaveChanges();
        }

    }
}