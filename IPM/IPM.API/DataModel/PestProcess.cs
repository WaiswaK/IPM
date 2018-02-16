using IPM.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace IPM.API.DataModel
{
    public class PestProcess
    {
        private static IPMEntities db = new IPMEntities();
        public static List<Pest> KeywordPests(string symptom)
        {
            return ClearPests(Pests(symptom));
        }
        public static List<Pest> ClearPests(List<Pest> pests)
        {
            List<Pest> manipulate = new List<Pest>();
            if (pests != null)
            {
                foreach (var pest in pests)
                {
                    Pest Search = manipulate.Find(x => x.P_ID.Contains(pest.P_ID));
                    if (Search == null)
                    {
                        manipulate.Add(pest);
                    }
                }
                return manipulate;
            }
            else
            {
                return null;
            }
        }
        private static List<Pest> Pests(string symptom)
        {
            List<Pest> final = new List<Pest>();
            Pest Pest = new Pest();
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
                    final = PestFromSymptom(id);
                }
            }
            return final;
        }
        private static List<Pest> PestFromSymptom(string symptom_id)
        {
            List<Pest> final = new List<Pest>();
            try
            {
                var query = db.Pest_Symptoms.Where(c => c.S_ID==symptom_id).ToList();
                foreach (var _result in query)
                {
                    string id = _result.P_ID;
                    Pest Pest = db.Pests.Where(c => c.P_ID==id).Single();
                    final.Add(Pest);
                }
            }
            catch
            {
            }
            return final;
        }    
        public static List<Solution> PestSolutions(string pest, bool control)
        {
            List<Solution> Final = new List<Solution>();
            if(control == false)
            {
                var query = db.Pest_Solutions.Where(c => c.P_ID == pest).ToList();
                foreach (var _result in query)
                {
                    Final.Add(db.Solutions.Where(c => c.Sol_ID == _result.Sol_ID).FirstOrDefault());
                }
            }
            else
            {
                var query = db.Pest_Solutions.Where(c => c.P_ID == pest).ToList();
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
        public static List<Pest> FinalPests(string selected_symptoms)
        {
            return ClearPests(ResponsePests(selected_symptoms));
        }
        private static List<Pest> PestsFromSelection(string symptom_id)
        {
            List<Pest> final = new List<Pest>();
            try
            {
                var query = db.Pest_Symptoms.Where(c => c.S_ID == symptom_id).ToList();
                if (query.Count == 1)
                {
                    foreach (var _result in query)
                    {
                        Pest pest = db.Pests.Where(c => c.P_ID == _result.P_ID).Single();
                        final.Add(pest);
                    }
                }
            }
            catch
            {
            }
            return final;
        }
        private static List<Pest> ResponsePests(string symptom)
        {
            List<Pest> final = new List<Pest>();
            Pest disease = new Pest();
            char[] delimiter = { '.' };
            List<string> split_symptoms = Constants.Data(symptom, delimiter);
            foreach (var split_symptom in split_symptoms)
            {
                List<Pest> symptom_pest = new List<Pest>();
                var query = db.Symptoms.FirstOrDefault(c => c.S_ID == split_symptom);
                if (query != null)
                {
                    symptom_pest = PestsFromSelection(query.S_ID);
                    if (symptom_pest != null)
                    {
                        foreach (var _pest in symptom_pest)
                        {
                            final.Add(_pest);
                        }
                    }
                }
            }
            return final;
        } 
    }
}