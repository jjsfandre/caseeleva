using CaseEleva.Models.SearchModel;
using CaseEleva.Models.ViewModel;
using CaseEleva.Repository;
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

        public List<EscolaViewModel> Search(EscolaSearchModel searchModel)
        {
            var escolas = EscolaRepository.GetEscolas();

            return escolas.Select(x => new EscolaViewModel
            {
                Id = x.Id
            }).ToList();
        }

        


    }
}