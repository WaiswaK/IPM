//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IPM.API.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class Pest_Symptom
    {
        [Display(Name = "Pest Symptom")]
        public string PS_ID { get; set; }
        [Display(Name = "Pest")]
        public string P_ID { get; set; }
        [Display(Name = "Symptom")]
        public string S_ID { get; set; }
        [Display(Name = "Part")]
        public string Part_ID { get; set; }
    
        public virtual Part Part { get; set; }
        public virtual Pest Pest { get; set; }
        public virtual Symptom Symptom { get; set; }
    }
}
