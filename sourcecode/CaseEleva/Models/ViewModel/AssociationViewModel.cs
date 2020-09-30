using System.Collections.Generic;

namespace CaseEleva.Models.ViewModel
{
    public class AssociationViewModel<TSearchModel, TViewModel>
    {
        public TSearchModel SearchModel { get; set; }
        public int TotalCount { get; set; }

        public List<TViewModel> Objects { get; set; }

        public AssociationViewModel() {
            this.Objects = new List<TViewModel>();
        }
    }
}