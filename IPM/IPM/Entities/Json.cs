using PDDT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Linq;
using Newtonsoft.Json.Linq;
using System;
using IPM.Models;

namespace PDDT.Entities
{
    class Json
    {
        #region Request
        public static async Task<List<Symptom>> GetConfirmSymptoms(string fruit, string malebud, string leaves, string stem,
            string corm, string root, string whole)
        {
            List<Symptom> found_symptoms = new List<Symptom>();
            string connected = await Plugin.Connectivity.CrossConnectivity.Current.
                IsRemoteReachable(Constants.baseUrl, Constants.port) ? "Reachable" : "Not reachable";

            if (connected == "Reachable")
            {
                var request_httpclient = new HttpClient();
                var request_postData = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("Fruit", Constants.NullRemove(fruit)),
                        new KeyValuePair<string, string>("Malebud", Constants.NullRemove(malebud)),
                        new KeyValuePair<string, string>("Leaves", Constants.NullRemove(leaves)),
                        new KeyValuePair<string, string>("stem", Constants.NullRemove(stem)),
                        new KeyValuePair<string, string>("Corm", Constants.NullRemove(corm)),
                        new KeyValuePair<string, string>("Root", Constants.NullRemove(root)),
                        new KeyValuePair<string, string>("Whole", Constants.NullRemove(whole))
                    };
                var request_formContent = new FormUrlEncodedContent(request_postData);
                var request_response = await request_httpclient.PostAsync(Constants.Json_link_request,
                                request_formContent);
                var request_result = await request_response.Content.ReadAsStreamAsync();
                var request_streamReader = new System.IO.StreamReader(request_result);
                var request_responseContent = request_streamReader.ReadToEnd().Trim().ToString();
                var request_symptom = JArray.Parse(request_responseContent);
                found_symptoms = Symptoms(request_symptom);
            }
            return found_symptoms;
        }
        private static List<Symptom> Symptoms(JArray SymptomArray)
        {
            List<Symptom> symptoms = new List<Symptom>();
            var SymptomList = (from i in SymptomArray select (i as JObject)).ToList();
            symptoms = SymptomObjects(SymptomList);
            return symptoms;
        }
        private static List<Symptom> SymptomObjects(List<JObject> All_Objects)
        {
            List<Symptom> symptoms = new List<Symptom>();
            foreach (var _object in All_Objects)
            {
                Symptom symptom = new Symptom()
                {
                    S_ID = _object.Value<string>("S_ID"),
                    Description = _object.Value<string>("Description"),
                    ImagePath = _object.Value<string>("ImagePath")
                };
                symptoms.Add(symptom);
            }
            return symptoms;
        }
        #endregion
        #region Respose
        public static async Task<List<Diagnosis>> GetDiagnosis(string selected_symptoms)
        {
            List<Diagnosis> diagnosis = new List<Diagnosis>();
            string connected = await Plugin.Connectivity.CrossConnectivity.Current.
                IsRemoteReachable(Constants.baseUrl, Constants.port) ? "Reachable" : "Not reachable";
            if (connected == "Reachable")
            {
                var response_httpclient = new HttpClient();
                var response_postData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("selected_symptoms", selected_symptoms)
                };
                var response_formContent = new FormUrlEncodedContent(response_postData);
                var response_response = await response_httpclient.PostAsync(Constants.Json_link_response, response_formContent);
                var response_result = await response_response.Content.ReadAsStreamAsync();
                var response_streamReader = new System.IO.StreamReader(response_result);
                var response_responseContent = response_streamReader.ReadToEnd().Trim().ToString();
                var response_diagnosis = JArray.Parse(response_responseContent);
                diagnosis = Diagnosis(response_diagnosis);
            }
            return diagnosis;
        }
        private static List<Diagnosis> Diagnosis(JArray diagnosisArray)
        {
            List<Diagnosis> diags = new List<Diagnosis>();
            var diagList = (from i in diagnosisArray select (i as JObject)).ToList();
            diags = DiagnosisObjects(diagList);
            return diags;
        }
        private static List<Diagnosis> DiagnosisObjects(List<JObject> All_Objects)
        {
            List<Diagnosis> diag = new List<Diagnosis>();
            foreach (var _object in All_Objects)
            {
                Diagnosis result = new Diagnosis()
                {
                    Item = _object.Value<string>("Item"),
                    Name = _object.Value<string>("Name"),
                    Solutions = Controls(_object.Value<JArray>("Solutions"))
                };
                diag.Add(result);
            }
            return diag;
        }
        #endregion
        #region PestList
        public static async Task<JArray> PestsArray()
        {
            var pest_httpclient = new HttpClient();
            var pest_response = await pest_httpclient.GetAsync(Constants.Json_link_pestlist);
            var pest_result = await pest_response.Content.ReadAsStreamAsync();
            var pest_streamReader = new System.IO.StreamReader(pest_result);
            var pest_responseContent = pest_streamReader.ReadToEnd().Trim().ToString();
            var pests = JArray.Parse(pest_responseContent);
            return pests;
        }
        public static List<Pest> PestList(JArray Array)
        {
            List<Pest> pests = new List<Pest>();
            foreach (var item in Array)
            {
                var obj = item as JObject;
                Pest pest = new Pest()
                {
                    ID = obj.Value<string>("ID"),
                    Name = obj.Value<string>("Name")
                };
                pests.Add(pest);
            }
            return pests;
        }
        public static async Task<JObject> PestObject(string id, string name)
        {
            JObject pestObject = new JObject();
            var client = new HttpClient();
            var postData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("ID", id),
                new KeyValuePair<string, string>("Name", name)
            };
            var formContent = new FormUrlEncodedContent(postData);
            var authresponse = await client.PostAsync(Constants.Json_link_pestlist, formContent);
            var authresult = await authresponse.Content.ReadAsStreamAsync();
            var authstreamReader = new System.IO.StreamReader(authresult);
            var authresponseContent = authstreamReader.ReadToEnd().Trim().ToString();
            pestObject = JObject.Parse(authresponseContent);
            return pestObject;
        }
        public static Pest PestDetails(JObject PestObject)
        {
            Pest pest = new Pest()
            {
                Name = PestObject.Value<string>("Name"),
                About = PestObject.Value<string>("About"),
                Spread = PestObject.Value<string>("Spread"),
                Controls = Controls(PestObject.Value<JArray>("Controls")),
                Symptoms = Symptoms(PestObject.Value<JArray>("Symptoms"))
            };
            return pest;
        }
        #endregion
        public static List<Control> Controls(JArray ControlArray)
        {
            List<Control> controls = new List<Control>();
            foreach (var item in ControlArray)
            {
                var obj = item as JObject;
                Control control = new Control()
                {
                    Cont = obj.Value<string>("Control"),
                    Description = obj.Value<string>("Description")
                };
                controls.Add(control);
            }
            return controls;
        }
        #region DiseaseList
        public static async Task<JArray> DiseasesArray()
        {
            var disease_httpclient = new HttpClient();
            var disease_response = await disease_httpclient.GetAsync(Constants.Json_link_diseaselist);
            var disease_result = await disease_response.Content.ReadAsStreamAsync();
            var disease_streamReader = new System.IO.StreamReader(disease_result);
            var disease_responseContent = disease_streamReader.ReadToEnd().Trim().ToString();
            var diseases = JArray.Parse(disease_responseContent);
            return diseases;
        }
        public static List<Disease> DiseaseList(JArray Array)
        {
            List<Disease> diseases = new List<Disease>();
            foreach (var item in Array)
            {
                var obj = item as JObject;
                Disease disease = new Disease()
                {
                    ID = obj.Value<string>("ID"),
                    Name = obj.Value<string>("Name")
                };
                diseases.Add(disease);
            }
            return diseases;
        }
        public static async Task<JObject> DiseaseObject(string id, string name)
        {
            JObject diseaseObject = new JObject();
            var client = new HttpClient();
            var postData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("ID", id),
                new KeyValuePair<string, string>("Name", name)
            };
            var formContent = new FormUrlEncodedContent(postData);
            var authresponse = await client.PostAsync(Constants.Json_link_diseaselist, formContent);
            var authresult = await authresponse.Content.ReadAsStreamAsync();
            var authstreamReader = new System.IO.StreamReader(authresult);
            var authresponseContent = authstreamReader.ReadToEnd().Trim().ToString();
            diseaseObject = JObject.Parse(authresponseContent);
            return diseaseObject;
        }
        public static Disease DiseaseDetails(JObject diseaseObject)
        {
            Disease disease = new Disease()
            {
                Name = diseaseObject.Value<string>("Name"),
                About = diseaseObject.Value<string>("About"),
                Spread = diseaseObject.Value<string>("Spread"),
                Controls = Controls(diseaseObject.Value<JArray>("Controls")),
                Symptoms = Symptoms(diseaseObject.Value<JArray>("Symptoms"))
            };
            return disease;
        }
        #endregion
        #region ChemicalList 
        public static async Task<JArray> ChemicalsArray()
        {
            var chemical_httpclient = new HttpClient();
            var chemical_response = await chemical_httpclient.GetAsync(Constants.Json_link_chemicallist);
            var chemical_result = await chemical_response.Content.ReadAsStreamAsync();
            var chemical_streamReader = new System.IO.StreamReader(chemical_result);
            var chemical_responseContent = chemical_streamReader.ReadToEnd().Trim().ToString();
            var chemicals = JArray.Parse(chemical_responseContent);
            return chemicals;
        }
        #endregion
        #region Login Json Methods
        public static LoginTokenResult GetLoginToken(string username, string password)
        {
            HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri(Constants.hostUrl)
            };
            HttpResponseMessage response =
        client.PostAsync("Token",
          new StringContent(string.Format("grant_type=password&username={0}&password={1}",
            username,
            password,
            "application/x-www-form-urlencoded"))).Result;
            string resultJSON = response.Content.ReadAsStringAsync().Result;
            LoginTokenResult result = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);
            return result;
        }
        #endregion
        #region Registration Methods
        public static async Task<Registration> GetLoginToken(string username, string password, string confirm)
        {
            Registration registration = new Registration();
            string connected = await Plugin.Connectivity.CrossConnectivity.Current.
                IsRemoteReachable(Constants.baseUrl, Constants.port) ? "Reachable" : "Not reachable";

            if (connected == "Reachable")
            {
                var request_httpclient = new HttpClient();
                var request_postData = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("Email", Constants.NullRemove(username)),
                        new KeyValuePair<string, string>("Password", Constants.NullRemove(password)),
                        new KeyValuePair<string, string>("ConfirmPassword", Constants.NullRemove(confirm))
                    };
                var request_formContent = new FormUrlEncodedContent(request_postData);
                var request_response = await request_httpclient.PostAsync(Constants.Json_link_register,
                                request_formContent);
                var request_result = await request_response.Content.ReadAsStreamAsync();
                var request_streamReader = new System.IO.StreamReader(request_result);
                var request_responseContent = request_streamReader.ReadToEnd().Trim().ToString();
                var request_reg = JObject.Parse(request_responseContent);
                registration = Registration(request_reg);
                return registration;
            }
            else
            {
                return null;
            }
        }
        private static Registration Registration(JObject RegistrationObject)
        {
            Registration reg = new Registration()
            {
                Message = RegistrationObject.Value<string>("Message"),
                Modelstate = ModelState(RegistrationObject.Value<JObject>("ModelState"))
            };
            return reg;
        }
        private static ModelState ModelState(JObject ModelStateObject)
        {
            ModelState modelstate = new ModelState()
            {
                Empty = EmailPassword(ModelStateObject, ""),
                Password = EmailPassword(ModelStateObject, "model.ConfirmPassword")

            };
            return modelstate;
        }
        private static string EmailPassword(JObject obj, string string_type)
        {
            try
            {
                return (string)obj[string_type][0];
            }
            catch
            {
                return string.Empty;
            }

        }
        #endregion
        #region Activity Methods
        public static async Task<string> ActivityUpload(DateTime date, string bunches,
            string weavils, string sales, string toppled)
        {
            string response = string.Empty;
            string connected = await Plugin.Connectivity.CrossConnectivity.Current.
                IsRemoteReachable(Constants.baseUrl, Constants.port) ? "Reachable" : "Not reachable";

            if (connected == "Reachable")
            {
                var request_httpclient = new HttpClient();
                var request_postData = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("Harvest_Date", date.ToString()),
                        new KeyValuePair<string, string>("Bunches", bunches),
                        new KeyValuePair<string, string>("Sales", sales),
                        new KeyValuePair<string, string>("Toppled_Plants", toppled),
                        new KeyValuePair<string, string>("Weavils_Noticed", weavils),
                        new KeyValuePair<string, string>("UserName",Database.UserDetails(Database.GetActiveUser()).User_name)
                    };
                var request_formContent = new FormUrlEncodedContent(request_postData);
                var request_response = await request_httpclient.PostAsync(Constants.Json_link_activity,
                                request_formContent);
                var request_result = await request_response.Content.ReadAsStreamAsync();
                var request_streamReader = new System.IO.StreamReader(request_result);
                var request_responseContent = request_streamReader.ReadToEnd().Trim().ToString();
                var request_symptom = JObject.Parse(request_responseContent);
                response = ActivityObject(request_symptom);
            }
            return response;
        }
        private static string ActivityObject(JObject activityObject)
        {
            string activity = activityObject.Value<string>("ManagementID");
            if (activity == null) return "Failed";
            else return "Activity saved successfully";
        }
        #endregion

    }
}
