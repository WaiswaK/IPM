using PDDT.Models;
using System.Collections.Generic;
using System.Text;

namespace PDDT.ViewModels
{
    class PestDiseaseViewModel
    {
        private string _name;
        private string _about;
        private string _chemical;
        private string _mechanical;
        private string _cultural;
        private string _biololgical;
        private string _spread;
        private bool _chemicalfound;
        private bool _biologicalfound;
        private bool _culturalfound;
        private bool _mechanicalfound;
        private string _symptoms;
        public string Name { get => _name; set => _name = value; }
        public string About { get => _about; set => _about = value; }
        public string Chemical { get => _chemical; set => _chemical = value; }
        public string Mechanical { get => _mechanical; set => _mechanical = value; }
        public string Cultural { get => _cultural; set => _cultural = value; }
        public string Biological { get => _biololgical; set => _biololgical = value; }
        public string Spread { get => _spread; set => _spread = value; }
        public bool ChemicalFound { get => _chemicalfound; set => _chemicalfound = value; }
        public bool BiologicalFound { get => _biologicalfound; set => _biologicalfound = value; }
        public bool MechanicalFound { get => _mechanicalfound; set => _mechanicalfound = value; }
        public bool CulturalFound { get => _culturalfound; set => _culturalfound = value; }
        public string Symptoms { get => _symptoms; set => _symptoms = value; }
        public PestDiseaseViewModel(Pest Retrieved)
        {
            Name = Retrieved.Name;
            About = Retrieved.About;
            Chemical = ControlsRetrieved("Chemical", Retrieved.Controls);
            Mechanical = ControlsRetrieved("Mechanical", Retrieved.Controls);
            Cultural = ControlsRetrieved("Cultural", Retrieved.Controls);
            Biological = ControlsRetrieved("Biological", Retrieved.Controls);
            Spread = Retrieved.Spread;
            Symptoms = SymptomsRetrieved(Retrieved.Symptoms);
            ChemicalFound = Found(ControlsRetrieved("Chemical", Retrieved.Controls));
            MechanicalFound = Found(ControlsRetrieved("Mechanical", Retrieved.Controls));
            CulturalFound = Found(ControlsRetrieved("Cultural", Retrieved.Controls));
            BiologicalFound = Found(ControlsRetrieved("Biological", Retrieved.Controls));
        }
        public PestDiseaseViewModel(Disease Retrieved)
        {
            Name = Retrieved.Name;
            About = Retrieved.About;
            Chemical = ControlsRetrieved("Chemical", Retrieved.Controls);
            Mechanical = ControlsRetrieved("Mechanical", Retrieved.Controls);
            Cultural = ControlsRetrieved("Cultural", Retrieved.Controls);
            Biological = ControlsRetrieved("Biological", Retrieved.Controls);
            Spread = Retrieved.Spread;
            Symptoms = SymptomsRetrieved(Retrieved.Symptoms);
            ChemicalFound = Found(ControlsRetrieved("Chemical", Retrieved.Controls));
            MechanicalFound = Found(ControlsRetrieved("Mechanical", Retrieved.Controls));
            CulturalFound = Found(ControlsRetrieved("Cultural", Retrieved.Controls));
            BiologicalFound = Found(ControlsRetrieved("Biological", Retrieved.Controls));
        }
        private string ControlsRetrieved(string description, List<Control> controls)
        {
            StringBuilder finalsb = new StringBuilder();
            foreach (var control in controls)
            {
                if (control.Cont == description)
                {
                    finalsb.Append(control.Description);
                    finalsb.Append(". ");
                    finalsb.Append("\n");
                }
            }
            return finalsb.ToString();
        }
        private bool Found(string text)
        {
            if (text == null || text == string.Empty)
                return false;
            else return true;
        }
        private string SymptomsRetrieved(List<Symptom> symptoms)
        {
            StringBuilder finalsb = new StringBuilder();
            int count = symptoms.Count;
            foreach (var symptom in symptoms)
            {
                count--;
                finalsb.Append(symptom.Description);
                finalsb.Append(". ");
                if (count > 0)
                {
                    finalsb.Append("\n\n");
                }
            }
            return finalsb.ToString();
        }
    }
}
