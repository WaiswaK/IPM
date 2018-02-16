using IPM.API.DataModel;
using IPM.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace IPM.API.Controllers
{
    public class ChemicalListController : ApiController
    {
        private IPMEntities db = new IPMEntities();
        // GET api/<controller>
        public IEnumerable<ControlResult> Get()
        {
            List<ControlResult> controls = new List<ControlResult>();
            List<Chemical> chemicals = db.Chemicals.ToList();
            foreach (var chemical in chemicals)
            {
                ControlResult control = new ControlResult()
                {
                    Control = "Chemical",
                    Description = chemical.Name
                };
                controls.Add(control);
            }
            return controls;
        }
    }
}