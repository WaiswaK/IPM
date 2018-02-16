using System.Collections.Generic;

namespace IPM.API.DataModel
{
    public class DiseasePestResult
    {
        public string Name { get; set; }
        public string About { get; set; }
        public string Spread { get; set; }
        public List<ControlResult> Controls { get; set; }
        public List<SymptomResult> Symptoms { get; set; }
    }
}