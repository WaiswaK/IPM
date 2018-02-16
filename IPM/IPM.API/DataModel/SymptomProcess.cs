using IPM.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IPM.API.DataModel
{
    public class SymptomProcess
    {
        private static IPMEntities db = new IPMEntities();
        public static List<Symptom> SymptomsFromKeyword(string fruit, string malebud,
            string leaves, string stem,
            string corm, string root, string whole)
        {
            List<Symptom> _symptoms = new List<Symptom>();
            List<Disease> _diseases = new List<Disease>();
            List<Pest> _pests = new List<Pest>();

            //Symtoms Retrieval
            List<Symptom> fruit_symptoms = SymptomsFromKeywords(Constants.RemoveEmptyString(fruit));
            List<Symptom> malebud_symptoms = SymptomsFromKeywords(Constants.RemoveEmptyString(malebud));
            List<Symptom> leaves_symptoms = SymptomsFromKeywords(Constants.RemoveEmptyString(leaves));
            List<Symptom> stem_symptoms = SymptomsFromKeywords(Constants.RemoveEmptyString(stem));
            List<Symptom> corm_symptoms = SymptomsFromKeywords(Constants.RemoveEmptyString(corm));
            List<Symptom> root_symptoms = SymptomsFromKeywords(Constants.RemoveEmptyString(root));
            List<Symptom> whole_symptoms = SymptomsFromKeywords(Constants.RemoveEmptyString(whole));

            foreach (var fruit_symptom in fruit_symptoms)
            {
                List<Disease> _ds = DiseaseProcess.KeywordDiseases(fruit_symptom.Description);
                List<Pest> _ps = PestProcess.KeywordPests(fruit_symptom.Description);
                if (_ds != null)
                {
                    foreach (var _disease in _ds)
                    {
                        _diseases.Add(_disease);
                    }
                }
                if (_ps != null)
                {
                    foreach (var _pest in _ps)
                    {
                        _pests.Add(_pest);
                    }
                }
            }
            foreach (var malebud_symptom in malebud_symptoms)
            {
                List<Disease> _ds = DiseaseProcess.KeywordDiseases(malebud_symptom.Description);
                List<Pest> _ps = PestProcess.KeywordPests(malebud_symptom.Description);
                if (_ds != null)
                {
                    foreach (var _disease in _ds)
                    {
                        _diseases.Add(_disease);
                    }
                }
                if (_ps != null)
                {
                    foreach (var _pest in _ps)
                    {
                        _pests.Add(_pest);
                    }
                }
            }
            foreach (var leaves_symptom in leaves_symptoms)
            {
                List<Disease> _ds = DiseaseProcess.KeywordDiseases(leaves_symptom.Description);
                List<Pest> _ps = PestProcess.KeywordPests(leaves_symptom.Description);
                if (_ds != null)
                {
                    foreach (var _disease in _ds)
                    {
                        _diseases.Add(_disease);
                    }
                }
                if (_ps != null)
                {
                    foreach (var _pest in _ps)
                    {
                        _pests.Add(_pest);
                    }
                }
            }
            foreach (var stem_symptom in stem_symptoms)
            {
                List<Disease> _ds = DiseaseProcess.KeywordDiseases(stem_symptom.Description);
                List<Pest> _ps = PestProcess.KeywordPests(stem_symptom.Description);
                if (_ds != null)
                {
                    foreach (var _disease in _ds)
                    {
                        _diseases.Add(_disease);
                    }
                }
                if (_ps != null)
                {
                    foreach (var _pest in _ps)
                    {
                        _pests.Add(_pest);
                    }
                }
            }
            foreach (var corm_symptom in corm_symptoms)
            {
                List<Disease> _ds = DiseaseProcess.KeywordDiseases(corm_symptom.Description);
                List<Pest> _ps = PestProcess.KeywordPests(corm_symptom.Description);
                if (_ds != null)
                {
                    foreach (var _disease in _ds)
                    {
                        _diseases.Add(_disease);
                    }
                }
                if (_ps != null)
                {
                    foreach (var _pest in _ps)
                    {
                        _pests.Add(_pest);
                    }
                }
            }
            foreach (var root_symptom in root_symptoms)
            {
                List<Disease> _ds = DiseaseProcess.KeywordDiseases(root_symptom.Description);
                List<Pest> _ps = PestProcess.KeywordPests(root_symptom.Description);
                if (_ds != null)
                {
                    foreach (var _disease in _ds)
                    {
                        _diseases.Add(_disease);
                    }
                }
                if (_ps != null)
                {
                    foreach (var _pest in _ps)
                    {
                        _pests.Add(_pest);
                    }
                }
            }
            foreach (var whole_symptom in whole_symptoms)
            {
                List<Disease> _ds = DiseaseProcess.KeywordDiseases(whole_symptom.Description);
                List<Pest> _ps = PestProcess.KeywordPests(whole_symptom.Description);
                if (_ds != null)
                {
                    foreach (var _disease in _ds)
                    {
                        _diseases.Add(_disease);
                    }
                }
                if (_ps != null)
                {
                    foreach (var _pest in _ps)
                    {
                        _pests.Add(_pest);
                    }
                }
            }
            List<Disease> _finaldiseases = DiseaseProcess.ClearDiseases(_diseases);
            List<Pest> _finalpests = PestProcess.ClearPests(_pests);
            return ClearSymptoms(RemoveSymptomRepetions(GenerateSymptoms(_finaldiseases, _finalpests)));
        }
        private static List<Symptom> SymptomsFromKeywords(string entry)
        {
            List<Symptom> _symptoms = new List<Symptom>();
            char[] delimiter = { ',' };
            char[] space = { ' ' };
            var query = db.Symptoms.ToList();
            foreach (var _result in query)
            {
                bool found = false;
                if (_result.Keywords != null)
                {
                    List<string> split_keywords = Constants.Data(_result.Keywords, delimiter);
                    foreach (var split in split_keywords)
                    {
                        string keywordfound = split.Trim();
                        int index = entry.IndexOf(keywordfound, StringComparison.OrdinalIgnoreCase);
                        if (index >= 0)
                        {
                            found = true;
                        }
                    }
                }
                if (found == true)
                {
                    _symptoms.Add(_result);
                }
            }
            return _symptoms;
        }
        private static List<Symptom> ClearSymptoms(List<Symptom> symptoms)
        {
            List<Symptom> Final = new List<Symptom>();
            foreach (var symptom in symptoms)
            {
                if (symptom.ImagePath == null || symptom.ImagePath == string.Empty)
                {

                }
                else
                {
                    Final.Add(symptom);
                }
            }
            return Final;
        }
        private static List<Symptom> RemoveSymptomRepetions(List<Symptom> symptoms)
        {
            List<Symptom> Final = new List<Symptom>();
            foreach (var symptom in symptoms)
            {
                Symptom Search = Final.Find(x => x.S_ID.Contains(symptom.S_ID));
                if (Search == null)
                {
                    Final.Add(symptom);
                }
            }
            return Final;
        }
        private static List<Symptom> GenerateSymptoms(List<Disease> diseases, List<Pest> pests)
        {
            List<Symptom> finalsymptoms = new List<Symptom>();
            try
            {
                if (diseases.Count > 0)
                {
                    foreach (var disease in diseases)
                    {
                        var query = db.Disease_Symptoms.Where(c => c.D_ID == disease.D_ID).ToList();
                        foreach (var symptom in query)
                        {
                            var sym = db.Symptoms.Where(c => c.S_ID == symptom.S_ID).ToList();
                            foreach (var finalout in sym)
                            {
                                finalsymptoms.Add(finalout);
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            try
            {
                if (pests.Count > 0)
                {
                    foreach (var pest in pests)
                    {
                        var query = db.Pest_Symptoms.Where(c => c.P_ID == pest.P_ID).ToList();
                        foreach (var symptom in query)
                        {
                            var sym = db.Symptoms.Where(c => c.S_ID == symptom.S_ID).ToList();
                            foreach (var finalout in sym)
                            {
                                finalsymptoms.Add(finalout);
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return finalsymptoms;
        }
        public static List<Symptom> SymptomsFromDisease(string disease_id)
        {
            List<Symptom> final = new List<Symptom>();
            var query = db.Disease_Symptoms.Where(c => c.D_ID == disease_id);
            foreach(var result in query)
            {
                Symptom symptom = db.Symptoms.FirstOrDefault(d => d.S_ID == result.S_ID);
                final.Add(symptom);
            }
            return RemoveSymptomRepetions(final);
        }
        public static List<Symptom> SymptomsFromPest(string pest_id)
        {
            List<Symptom> final = new List<Symptom>();
            var query = db.Pest_Symptoms.Where(c => c.P_ID == pest_id);
            foreach (var result in query)
            {
                Symptom symptom = db.Symptoms.FirstOrDefault(d => d.S_ID == result.S_ID);
                final.Add(symptom);
            }
            return RemoveSymptomRepetions(final);
        }
        public static List<SymptomResult> Symptoms(List<Symptom> symptoms)
        {
            List<SymptomResult> final = new List<SymptomResult>();
            foreach (var symptom in symptoms)
            {
                SymptomResult jsonsymptom = new SymptomResult()
                {
                    S_ID = symptom.S_ID,
                    Description = symptom.Description,
                    ImagePath = symptom.ImagePath
                };
                final.Add(jsonsymptom);
            }
            return final;
        }
    }
}