using IPM.API.DataModel;
using IPM.API.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPM.API.Controllers
{
    public class RequestController : ApiController
    {
        private IPMEntities db = new IPMEntities();
        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] RequestData requestdata)
        {
            List<SymptomResult> finalsymptoms = new List<SymptomResult>();
            List<Symptom> symptoms = SymptomProcess.SymptomsFromKeyword(requestdata.Fruit,
                requestdata.Malebud, requestdata.Leaves, requestdata.Stem, requestdata.Corm,
                requestdata.Root, requestdata.Whole);                  
            try
            {
                if (symptoms.Count > 0)
                {
                    foreach (var symptom in symptoms)
                    {
                        SymptomResult jsonsymptom = new SymptomResult()
                        {
                            S_ID = symptom.S_ID,
                            Description = symptom.Description,
                            ImagePath = symptom.ImagePath
                        };
                        finalsymptoms.Add(jsonsymptom);
                    }
                }
            }
            catch
            {
            }
                    
            if (finalsymptoms.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, finalsymptoms);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Diagnosis found");
            }
        }
    }
}