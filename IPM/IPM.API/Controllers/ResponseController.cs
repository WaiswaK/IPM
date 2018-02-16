using IPM.API.DataModel;
using IPM.API.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPM.API.Controllers
{
    public class ResponseController : ApiController
    {          
        private IPMEntities db = new IPMEntities();
        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] ResponseData responsedata)
        {
            List<Disease> diseases = DiseaseProcess.FinalDiseases(responsedata.Selected_symptoms);
            List<Pest> pests = PestProcess.FinalPests(responsedata.Selected_symptoms);
            List<Result> final = new List<Result>();
            try
            {
                foreach (var disease in diseases)
                {
                    Result dis = new Result()
                    {
                        Item = "Disease",
                        Name = disease.Name,
                        Solutions = ControlProcess.DiseaseControls(disease.D_ID)
                    };
                    final.Add(dis);
                }
            }
            catch
            {

            }
            try
            {
                foreach (var pest in pests)
                {
                    Result pes = new Result()
                    {
                        Item = "Pest",
                        Name = pest.Name,
                        Solutions = ControlProcess.PestControls(pest.P_ID)
                    };
                    final.Add(pes);
                }
            }
            catch
            {

            }
            if (diseases == null && pests == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Diagnosis found");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, final);
            }
        }
        
    }
}