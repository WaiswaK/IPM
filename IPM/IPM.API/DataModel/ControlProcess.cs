using IPM.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace IPM.API.DataModel
{
    public class ControlProcess
    {
        private static IPMEntities db = new IPMEntities();
        public static List<ControlResult> PestControls(string pest_id)
        {
            List<Solution> solutions = PestProcess.PestSolutions(pest_id, false);
            List<ControlResult> controls = new List<ControlResult>();
            foreach (var solution in solutions)
            {
                ControlResult control = new ControlResult()
                {
                    Control = db.Controls.FirstOrDefault(c => c.C_ID == solution.C_ID).Description,
                    Description = solution.Description
                };
                controls.Add(control);
            }
            return controls;
        }
        public static List<ControlResult> DiseaseControls(string disease_id)
        {
            List<Solution> solutions = DiseaseProcess.DiseaseSolutions(disease_id, false);
            List<ControlResult> controls = new List<ControlResult>();
            foreach (var solution in solutions)
            {
                ControlResult control = new ControlResult()
                {
                    Control = db.Controls.FirstOrDefault(c => c.C_ID == solution.C_ID).Description,
                    Description = solution.Description
                };
                controls.Add(control);
            }
            return controls;
        }
    }
}