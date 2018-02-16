using PDDT.Models;
using System.Collections.Generic;
using System.Text;

namespace PDDT.ViewModels
{
    class DiagnosisViewModel
    {
        private string _pestchemical;
        private string _pestmechanical;
        private string _pestcultural;
        private string _pestbiololgical;
        private bool _pestchemicalfound;
        private bool _pestbiologicalfound;
        private bool _pestculturalfound;
        private bool _pestmechanicalfound;
        private string _diseasechemical;
        private string _diseasemechanical;
        private string _diseasecultural;
        private string _diseasebiololgical;
        private bool _diseasechemicalfound;
        private bool _diseasebiologicalfound;
        private bool _diseaseculturalfound;
        private bool _diseasemechanicalfound;
        private string _pest;
        public string Pestname
        {
            get => _pest; set => _pest = value;
        }
        private string _disease;
        public string Diseasename
        {
            get => _disease; set => _disease = value;
        } 
        private string _selected;
        public string Selected_symptom
        {
            get => _selected; set => _selected = value;
        }
        private List<Diagnosis> _diag;
        public List<Diagnosis> Diag
        {
            get => _diag; set => _diag = value;
        }
        private bool _pests;
        public bool PestsFound
        {
            get => _pests; set => _pests = value;
        }
        private bool _diseases;
        public bool DiseasesFound
        {
            get => _diseases; set => _diseases = value;
        }
        public IEnumerable<Control> Chemicals { get; }
        public string PestChemical { get => _pestchemical; set => _pestchemical = value; }
        public string PestMechanical { get => _pestmechanical; set => _pestmechanical = value; }
        public string PestCultural { get => _pestcultural; set => _pestcultural = value; }
        public string PestBiological { get => _pestbiololgical; set => _pestbiololgical = value; }
        public bool PestChemicalFound { get => _pestchemicalfound; set => _pestchemicalfound = value; }
        public bool PestBiologicalFound { get => _pestbiologicalfound; set => _pestbiologicalfound = value; }
        public bool PestMechanicalFound { get => _pestmechanicalfound; set => _pestmechanicalfound = value; }
        public bool PestCulturalFound { get => _pestculturalfound; set => _pestculturalfound = value; }
        public string DiseaseChemical { get => _diseasechemical; set => _diseasechemical = value; }
        public string DiseaseMechanical { get => _diseasemechanical; set => _diseasemechanical = value; }
        public string DiseaseCultural { get => _diseasecultural; set => _diseasecultural = value; }
        public string DiseaseBiological { get => _diseasebiololgical; set => _diseasebiololgical = value; }
        public bool DiseaseChemicalFound { get => _diseasechemicalfound; set => _diseasechemicalfound = value; }
        public bool DiseaseBiologicalFound { get => _diseasebiologicalfound; set => _diseasebiologicalfound = value; }
        public bool DiseaseMechanicalFound { get => _diseasemechanicalfound; set => _diseasemechanicalfound = value; }
        public bool DiseaseCulturalFound { get => _diseaseculturalfound; set => _diseaseculturalfound = value; }
        public DiagnosisViewModel(string selected, List<Control> chemicals)
        {
            Selected_symptom = selected;
            Chemicals = chemicals;
        }
        public DiagnosisViewModel(List<Diagnosis> final_diag, string chemical_selected)
        {
            Diag = final_diag;
            DiseasesFound = DiseasesSearch(final_diag);
            PestsFound = PestsSearch(final_diag);
            Diseasename = DiseaseName(final_diag);
            Pestname = PestName(final_diag);
            PestBiological = ControlsRetrieved("Biological", final_diag, "Pest", chemical_selected);
            PestBiologicalFound = Found(ControlsRetrieved("Biological", final_diag, "Pest", chemical_selected), PestsSearch(final_diag));
            PestChemical = ControlsRetrieved("Chemical", final_diag, "Pest", chemical_selected);
            PestChemicalFound = Found(ControlsRetrieved("Chemical", final_diag, "Pest", chemical_selected), PestsSearch(final_diag));
            PestCultural = ControlsRetrieved("Cultural", final_diag, "Pest", chemical_selected);
            PestCulturalFound = Found(ControlsRetrieved("Cultural", final_diag, "Pest", chemical_selected), PestsSearch(final_diag));
            PestMechanical = ControlsRetrieved("Mechanical", final_diag, "Pest", chemical_selected);
            PestMechanicalFound = Found(ControlsRetrieved("Mechanical", final_diag, "Pest", chemical_selected), PestsSearch(final_diag));
            DiseaseBiological = ControlsRetrieved("Biological", final_diag, "Disease", chemical_selected);
            DiseaseBiologicalFound = Found(ControlsRetrieved("Biological", final_diag, "Disease", chemical_selected), DiseasesSearch(final_diag));
            DiseaseChemical = ControlsRetrieved("Chemical", final_diag, "Disease", chemical_selected);
            DiseaseChemicalFound = Found(ControlsRetrieved("Chemical", final_diag, "Disease", chemical_selected), DiseasesSearch(final_diag));
            DiseaseCultural = ControlsRetrieved("Cultural", final_diag, "Disease", chemical_selected);
            DiseaseCulturalFound = Found(ControlsRetrieved("Cultural", final_diag, "Disease", chemical_selected), DiseasesSearch(final_diag));
            DiseaseMechanical = ControlsRetrieved("Mechanical", final_diag, "Disease", chemical_selected);
            DiseaseMechanicalFound = Found(ControlsRetrieved("Mechanical", final_diag, "Disease", chemical_selected), DiseasesSearch(final_diag));
        }
        private bool PestsSearch(List<Diagnosis> final_diag)
        {
            bool found = false;
            foreach (var diag in final_diag)
            {
                if (diag.Item == "Pest")
                {
                    found = true;
                }
            }
            return found;
        }
        private bool DiseasesSearch(List<Diagnosis> final_diag)
        {
            bool found = false;
            foreach (var diag in final_diag)
            {
                if (diag.Item == "Disease")
                {
                    found = true;
                }
            }
            return found;
        }
        private string DiseaseName(List<Diagnosis> final_diag)
        {
            if (DiseasesSearch(final_diag) == false)
            {
                return string.Empty;
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                string final = string.Empty;
                foreach (var diag in final_diag)
                {
                    if (diag.Item == "Disease")
                    {
                        if (final == string.Empty)
                        {
                            final = diag.Name;
                        }
                        else
                        {
                            sb.Append(" and");
                            sb.Append(" " + diag.Name);
                        }
                    }
                }
                StringBuilder finalsb = new StringBuilder();
                finalsb.Append(final);
                finalsb.Append(sb.ToString());
                return finalsb.ToString();
            }
        }
        private string PestName(List<Diagnosis> final_diag)
        {
            if (PestsSearch(final_diag) == false)
            {
                return string.Empty;
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                string final = string.Empty;
                foreach (var diag in final_diag)
                {
                    if (diag.Item == "Pest")
                    {
                        if (final == string.Empty)
                        {
                            final = diag.Name;
                        }
                        else
                        {
                            sb.Append(" and");
                            sb.Append(" " + diag.Name);
                        }
                    }
                }
                StringBuilder finalsb = new StringBuilder();
                finalsb.Append(final);
                finalsb.Append(sb.ToString());
                return finalsb.ToString();
            }
        }
        private string ControlsRetrieved(string description, List<Diagnosis> diags, string type, string chemical_selected)
        {
            StringBuilder finalsb = new StringBuilder();
            foreach (var diag in diags)
            {
                if (diag.Item == type )
                {
                    List<Control> controls = diag.Solutions;
                    foreach(var control in controls)
                    {
                        if (control.Cont == description)
                        {
                            if(control.Description!= chemical_selected)
                            {
                                finalsb.Append(control.Description);
                                finalsb.Append(". ");
                            }                          
                        }
                    }
                }
            }
            return finalsb.ToString();
        }
        private bool Found(string text, bool master)
        {
            if (master == true)
            {
                if (text == null || text == string.Empty)
                    return false;
                else return true;
            }
            else return false;
            
        }
    }
}
