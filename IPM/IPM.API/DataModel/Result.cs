using System.Collections.Generic;

namespace IPM.API.DataModel
{
    public class Result
    {
        public string Name { get; set; }
        public List<ControlResult> Solutions { get; set; }
        public string Item { get; set; }     
    }
}