using IPM.API.DataModel;
using IPM.API.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IPM.API.Controllers
{
    public class ActivitiesController : ApiController
    {
        private IPMEntities db = new IPMEntities();
        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]ActivityData activity)
        {
            var query = db.Managements.Count() + 1;
            bool exist = false;
            string temp = "MAN-" + query;
            Management process = new Management()
            {
                ManagementID = temp,
                Bunches = activity.Bunches,
                Harvest_Date = activity.Harvest_Date,
                Sales = activity.Sales,
                Toppled_Plants = activity.Toppled_Plants,
                Weavils_Noticed = activity.Weavils_Noticed,
                Id = Constants.User_ID(activity.UserName)
            };

            var manage = db.Managements.FirstOrDefault(c => c.ManagementID == temp);
            if (manage != null)
                exist = true;

            if (exist)
            {
                var all = db.Managements.ToList();
                var dis = all.Last();
                process.ManagementID = "MAN-" + Constants.NextNumber(dis.ManagementID);
            }
            try
            {
                db.Managements.Add(process);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created, process);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}