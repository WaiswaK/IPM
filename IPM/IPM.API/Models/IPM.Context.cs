﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IPM.API.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class IPMEntities : DbContext
    {
        public IPMEntities()
            : base("name=IPMEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Chemical> Chemicals { get; set; }
        public virtual DbSet<Control> Controls { get; set; }
        public virtual DbSet<Disease> Diseases { get; set; }
        public virtual DbSet<Disease_Solution> Disease_Solutions { get; set; }
        public virtual DbSet<Disease_Symptom> Disease_Symptoms { get; set; }
        public virtual DbSet<Management> Managements { get; set; }
        public virtual DbSet<Part> Parts { get; set; }
        public virtual DbSet<Pest> Pests { get; set; }
        public virtual DbSet<Pest_Solution> Pest_Solutions { get; set; }
        public virtual DbSet<Pest_Symptom> Pest_Symptoms { get; set; }
        public virtual DbSet<Solution> Solutions { get; set; }
        public virtual DbSet<Symptom> Symptoms { get; set; }
    }
}
