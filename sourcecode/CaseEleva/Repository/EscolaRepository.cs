using CaseEleva.Models;
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

        public IQueryable<Escola> GetEscolas()
        {
            var context = DBFactory.GetInstance().GetDb();
            return context.Escola.AsQueryable();
        }
    }
}