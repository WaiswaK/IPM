using PDDT.Entities;
using PDDT.Models;
using System.Collections.Generic;
using System.Linq;

namespace PDDT.ViewModels
{
    class SymptomConfirmViewModel 
    {
        private List<Symptom> _symptoms;
        public List<Symptom> Symptoms
        {
            get => _symptoms; set => _symptoms = value;
        }
        public SymptomConfirmViewModel(List<Symptom> Confirmstion_Symptoms)
        {
            Symptoms = ImageCleared(Confirmstion_Symptoms);
        }
        private List<Symptom> ImageCleared(List<Symptom> symptoms)
        {
            List<Symptom> Final = new List<Symptom>();
            foreach (var _symptom in symptoms)
            {
                if (_symptom.ImagePath == null)
                {

                }
                else
                {
                    _symptom.ImagePath = Imagepath(_symptom.ImagePath);
                    Final.Add(_symptom);
                }
            }

            return Final;
        }
        private static string Imagepath(string path)
        {
            string imagepath = string.Empty;
            char[] delimiter = { '~' };
            string[] linksplit = path.Split(delimiter);
            List<string> linklist = linksplit.ToList();
            imagepath = Constants.hostUrl + linklist.Last();
            return imagepath;
        }
    }
}
