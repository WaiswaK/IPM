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

    public partial class Pest_Solution
    {
        [Display(Name = "Pest Solution")]
        public string PSol_ID { get; set; }
        [Display(Name = "Pest")]
        public string P_ID { get; set; }
        [Display(Name = "Solution")]
        public string Sol_ID { get; set; }
    
        public virtual Pest Pest { get; set; }
        public virtual Solution Solution { get; set; }
    }
}