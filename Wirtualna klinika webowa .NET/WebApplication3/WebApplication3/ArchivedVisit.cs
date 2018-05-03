//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ArchivedVisit
    {
        public int Id { get; set; }
        [Display(Name = "Data")]
        [Required]
        public System.DateTime Date { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        [Display(Name = "Ocena")]
        [Range(0,5, ErrorMessage = "Ocena mo�e by� z zakresu od 0 do 5.")]
        public Nullable<int> Rating { get; set; }
        [Display(Name = "Doktor")]
        [Required]
        public virtual Doctor Doctor { get; set; }
        [Display(Name = "Pacjent")]
        [Required]
        public virtual Patient Patient { get; set; }
    }
}