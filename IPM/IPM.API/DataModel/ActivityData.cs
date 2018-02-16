using System;

namespace IPM.API.DataModel
{
    public class ActivityData
    {
        public DateTime Harvest_Date { get; set; }
        public decimal Bunches { get; set; }
        public decimal Sales { get; set; }
        public decimal Toppled_Plants { get; set; }
        public decimal Weavils_Noticed { get; set; }
        public string UserName { get; set; }
    }
}