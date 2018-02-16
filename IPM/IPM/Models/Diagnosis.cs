using System.Collections.Generic;

namespace PDDT.Models
{
    class Diagnosis
    {
        public string Name { get; set; }
        public List<Control> Solutions { get; set; }
        public string Item { get; set; }
    }
}
