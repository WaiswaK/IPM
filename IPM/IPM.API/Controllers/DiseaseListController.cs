using IPM.API.DataModel;
using IPM.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPM.API.Controllers
{
    public class DiseaseListController : ApiController
    {
        private IPMEntities db = new IPMEntities();
        // GET api/<controller>
        public IEnumerable<DiseasePestData> Get()
        {
            List<Disease> diseases = db.Diseases.ToList();
            List<DiseasePestData> final = new List<DiseasePestData>();
            foreach (var disease in diseases)
            {
                DiseasePestData temp = new DiseasePestData()
                {
                    ID = disease.D_ID,
                    Name = disease.Name
                };
                final.Add(temp);
            }
            return final;
        }
        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]DiseasePestData diseasedata)
        {
            var query = db.Diseases.FirstOrDefault(c => c.D_ID == diseasedata.ID);
            List<ControlResult> pestcontrols = ControlProcess.DiseaseControls(diseasedata.ID);
            DiseasePestResult disease = new DiseasePestResult()
            {
                About = query.About,
                Controls = pestcontrols,
                Name = query.Name,
                Spread = query.Transmision,
                Symptoms = SymptomProcess.Symptoms(SymptomProcess.SymptomsFromDisease(query.D_ID))
            };
            return Request.CreateResponse(HttpStatusCode.OK, disease);
        }
    }
}