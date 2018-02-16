using PDDT.Models;
using System.Collections.Generic;

namespace PDDT.ViewModels
{
    class PestsDiseasesViewModel
    {
        private List<Disease> _diseases;
        public List<Disease> Diseases
        {
            get => _diseases; set => _diseases = value;
        }
        private List<Pest> _pests;
        public List<Pest> Pests
        {
            get => _pests; set => _pests = value;
        }
        public PestsDiseasesViewModel(List<Disease> diseases)
        {
            Diseases = diseases;
        }
        public PestsDiseasesViewModel(List<Pest> pests)
        {
            Pests = pests;
        }
    }
}
