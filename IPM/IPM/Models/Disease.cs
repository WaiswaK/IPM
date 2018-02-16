using System.Collections.Generic;

namespace PDDT.Models
{
    class Disease
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public string Spread { get; set; }
        public List<Control> Controls { get; set; }
        public List<Symptom> Symptoms { get; set; }
    }
}
