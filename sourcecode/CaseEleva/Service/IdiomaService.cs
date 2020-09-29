using CaseEleva.Models;
using System.Collections.Generic;
using System.Linq;

namespace CaseEleva.Service
{
    public class IdiomaService
    {
        private static IdiomaService _instance;

        private IdiomaService() { }

        public static IdiomaService GetInstance()
        {
            if (_instance == null)
                _instance = new IdiomaService();

            return _instance;
        }

        public List<Idioma> GetAll()
        {
            return DBFactory.GetInstance().GetDb().Idioma.ToList();
        }

        public void AlterarIdioma(int idiomaId)
        {
            var context = DBFactory.GetInstance().GetDb();
            context.Configuracao.SingleOrDefault().IdiomaId = idiomaId;
            context.SaveChanges();
        }
    }
}