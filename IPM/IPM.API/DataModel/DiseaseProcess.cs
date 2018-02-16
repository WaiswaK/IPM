using IPM.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace IPM.API.DataModel
{
    public class DiseaseProcess
    {
        private static IPMEntities db = new IPMEntities();
        public static List<Disease> KeywordDiseases(string symptom)
        {
            return ClearDiseases(Diseases(symptom));
        }
        public static List<Disease> ClearDiseases(List<Disease> diseases)
        {
            List<Disease> manipulate = new List<Disease>();
            if (diseases!=null)
            { 
                foreach (var disease in diseases)
                {
                    Disease Search = manipulate.Find(x => x.D_ID.Contains(disease.D_ID));
                    if (Search == null)
                    {
                        manipulate.Add(disease);
                    }
                }
                return manipulate;
            }
            else
            {
                return null;
            }
        }
        private static List<Disease> Diseases(string symptom)
        {
            List<Disease> final = new List<Disease>();
            Disease disease = new Disease();
            char[] delimiter = { '.' };
            List<string> split_symptoms = Constants.Data(symptom, delimiter);
            foreach (var split_symptom in split_symptoms)
            {
                string id = string.Empty;
                try
                {
                    var query = db.Symptoms.Where(c => c.Description == split_symptom).Single();
                    id = query.S_ID;
                }
                catch
                {
                }

                if (id.Equals(string.Empty))
                {

                }
                else
                {
                    final = DiseasesFromSymptom(id);
                }
            }
            return final;
        }       
        private static List<Disease> DiseasesFromSymptom(string symptom_id)
        {
            List<Disease> final = new List<Disease>();
            try
            {
                var query = db.Disease_Symptoms.Where(c => c.S_ID.Equals(symptom_id)).ToList();
                foreach (var _result in query)
                {
                    string id = _result.D_ID;
                    Disease disease = db.Diseases.Where(c => c.D_ID.Equals(id)).Single();
                    final.Add(disease);
                }
            }
            catch
            {
            }
            return final;
        }   
        public static List<Solution> DiseaseSolutions(string disease, bool control)
        {
            List<Solution> Final = new List<Solution>();
            if(control == false)
            {
                var query = db.Disease_Solutions.Where(c => c.D_ID == disease).ToList();
                foreach (var _result in query)
                {
                    Final.Add(db.Solutions.Where(c => c.Sol_ID == _result.Sol_ID).FirstOrDefault());
                }
            }
            else
            {
                var query = db.Disease_Solutions.Where(c => c.D_ID == disease).ToList();
                foreach (var _result in query)
                {
                    var sol = db.Solutions.Where(c => c.Sol_ID == _result.Sol_ID).FirstOrDefault();
                    if (sol.C_ID != "C-2")
                    {
                        Final.Add(sol);
                    }
                }
            }              
            return Final;
        }
        public static List<Disease> FinalDiseases(string selected_symptoms)
        {            
            return ClearDiseases(ResponseDiseases(selected_symptoms));
        }
        private static List<Disease> DiseasesFromSelection(string symptom_id)
        {
            List<Disease> final = new List<Disease>();
            try
            {
                var query = db.Disease_Symptoms.Where(c => c.S_ID == symptom_id).ToList();
                if (query.Count==1)
                {
                    foreach (var _result in query)
                    {
                        string id = _result.D_ID;
                        Disease disease = db.Diseases.Where(c => c.D_ID.Equals(id)).Single();
                        final.Add(disease);
                    }
                }              
            }
            catch
            {
            }
            return final;
        }
        private static List<Disease> ResponseDiseases(string symptom)
        {
            List<Disease> final = new List<Disease>();         
            Disease disease = new Disease();
            char[] delimiter = { '.' };
            List<string> split_symptoms = Constants.Data(symptom, delimiter);
            foreach (var split_symptom in split_symptoms)
            {
                List<Disease> list_symptom = new List<Disease>();
                var query = db.Symptoms.FirstOrDefault(c => c.S_ID == split_symptom);
                if (query != null)
                {
                    list_symptom = DiseasesFromSelection(query.S_ID);
                    if (list_symptom != null)
                    {
                        foreach (var _disease in list_symptom)
                        {
                            final.Add(_disease);
                        }
                    }
                }
            }
            return final;
        }
    }
}