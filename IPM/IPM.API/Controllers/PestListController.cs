using IPM.API.DataModel;
using IPM.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPM.API.Controllers
{
    public class PestListController : ApiController
    {
        private IPMEntities db = new IPMEntities();
        // GET api/<controller>
        public IEnumerable<DiseasePestData> Get()
        {
            List<Pest> pests = db.Pests.ToList();
            List<DiseasePestData> final = new List<DiseasePestData>();
            foreach (var pest in pests)
            {
                DiseasePestData temp = new DiseasePestData()
                {
                    ID = pest.P_ID,
                    Name = pest.Name
                };
                final.Add(temp);
            }
            return final;
        }
        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]DiseasePestData pestdata)
        {
            var query = db.Pests.FirstOrDefault(c => c.P_ID == pestdata.ID);
            List<ControlResult> pestcontrols = ControlProcess.PestControls(pestdata.ID);
            DiseasePestResult pest = new DiseasePestResult()
            {
                About = query.About,
                Controls = pestcontrols,
                Name = query.Name,
                Spread = query.Spread,
                Symptoms = SymptomProcess.Symptoms(SymptomProcess.SymptomsFromPest(query.P_ID))
            };
            return Request.CreateResponse(HttpStatusCode.OK, pest);
        }
    }
}