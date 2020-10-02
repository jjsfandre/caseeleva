using CaseEleva.Models;
using CaseEleva.Models.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace CaseEleva.Repository
{
    public class EscolaRepository
    {

        private static EscolaRepository _instance;

        private EscolaRepository() { }

        public static EscolaRepository GetInstance()
        {
            if (_instance == null)
                _instance = new EscolaRepository();

            return _instance;
        }

        public IQueryable<Escola> GetAll()
        {
            var context = DBFactory.GetInstance().GetDb();
            return context.Escola.AsQueryable();
        }

        public void Save(EscolaViewModel formModel)
        {
            var context = DBFactory.GetInstance().GetDb();
            Escola escola;
            if (formModel.Id.HasValue)
            {
                escola = GetById(formModel.Id.Value, context);
            }
            else
            {
                escola = new Escola();
                context.Escola.Add(escola);
            }
            escola.Logradouro = formModel.Logradouro;
            escola.Nome = formModel.Nome;
            escola.Numero = formModel.Numero;
            escola.Telefone = formModel.Telefone;
            escola.Complemento = formModel.Complemento;
            escola.Cidade = formModel.Cidade;
            escola.Diretor = formModel.Diretor;
            escola.Estado = formModel.Estado;
            context.SaveChanges();
        }

        public Escola GetById(int id, CaseElevaEntities context = null)
        {
            if (context == null)
                context = DBFactory.GetInstance().GetDb();

            return context.Escola.FirstOrDefault(x => x.Id == id);
        }

        public List<Escola> GetByIds(int[] ids, CaseElevaEntities context = null)
        {
            if (context == null)
                context = DBFactory.GetInstance().GetDb();

            return context.Escola.Where(x => ids.Contains(x.Id)).ToList();
        }

        public void DeleteByIds(int[] ids)
        {
            var context = DBFactory.GetInstance().GetDb();
            List<Escola> escolas = GetByIds(ids, context);
            context.Escola.RemoveRange(escolas);
            context.SaveChanges();

        }
    }
}