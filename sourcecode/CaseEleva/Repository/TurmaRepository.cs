using CaseEleva.Models;
using CaseEleva.Models.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace CaseEleva.Repository
{
    public class TurmaRepository
    {

        private static TurmaRepository _instance;

        private TurmaRepository() { }

        public static TurmaRepository GetInstance()
        {
            if (_instance == null)
                _instance = new TurmaRepository();

            return _instance;
        }

        public IQueryable<Turma> GetAll()
        {
            var context = DBFactory.GetInstance().GetDb();
            return context.Turma.AsQueryable();
        }

        public void Save(TurmaViewModel formModel)
        {
            var context = DBFactory.GetInstance().GetDb();
            Turma Turma;
            if (formModel.Id.HasValue)
            {
                Turma = GetById(formModel.Id.Value, context);
            }
            else
            {
                Turma = new Turma();
                context.Turma.Add(Turma);
            }
            Turma.Codigo = formModel.Codigo;
            Turma.EscolaId = formModel.EscolaId.Value;
            Turma.Professor = formModel.Professor;
            Turma.TotalAlunos = formModel.TotalAlunos.Value;
            Turma.Descricao = formModel.Descricao;
            context.SaveChanges();
        }

        public Turma GetById(int id, CaseElevaEntities context = null)
        {
            if (context == null)
                context = DBFactory.GetInstance().GetDb();

            return context.Turma.FirstOrDefault(x => x.Id == id);
        }

        public List<Turma> GetByIds(int[] ids, CaseElevaEntities context = null)
        {
            if (context == null)
                context = DBFactory.GetInstance().GetDb();

            return context.Turma.Where(x => ids.Contains(x.Id)).ToList();
        }

        public List<Turma> GetByEscolaIds(int[] ids, CaseElevaEntities context = null)
        {
            if (context == null)
                context = DBFactory.GetInstance().GetDb();

            return context.Turma.Where(x => ids.Contains(x.EscolaId)).ToList();
        }

        public void DeleteByIds(int[] ids)
        {
            var context = DBFactory.GetInstance().GetDb();
            List<Turma> Turmas = GetByIds(ids, context);
            context.Turma.RemoveRange(Turmas);
            context.SaveChanges();

        }

        public void DeleteByEscolaIds(int[] ids, CaseElevaEntities context = null, bool isToSaveContext = true)
        {
            if (context == null)
                context = DBFactory.GetInstance().GetDb();

            List<Turma> Turmas = GetByEscolaIds(ids, context);
            context.Turma.RemoveRange(Turmas);

            if (isToSaveContext)
                context.SaveChanges();
        }

        //TurmaRepository = TurmaRepository.GetInstance();
    }
}