﻿//------------------------------------------------------------------------------
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
    
    public partial class Clinic
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Clinic()
        {
            this.Doctors = new HashSet<Doctor>();
        }

        
        public int Id { get; set; }
        [Display(Name = "Nazwa")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Państwo")]
        [Required]
        [RegularExpression(@"^([A-ZĄĘŹŁŃŻŚĆÓ][a-zżźćąęłńśó]{2,40})([ -][A-ZĄĘŹŁŃŻŚĆÓ][a-zżźćąęłńśó]{2,40}){0,2}$", ErrorMessage = "Nazwa państwa musi zaczynać się wielką literą")]
        public string ClinicAddress_Country { get; set; }
        [Display(Name = "Miasto")]
        [Required]
        [RegularExpression(@"^([A-ZĄĘŹŁŃŻŚĆÓ][a-zżźćąęłńśó]{2,40})([ -][A-ZĄĘŹŁŃŻŚĆÓ][a-zżźćąęłńśó]{2,40}){0,2}$", ErrorMessage = "Nazwa miejscowości musi zaczynać się wielką literą")]
        public string ClinicAddress_City { get; set; }
        [Display(Name = "Ulica")]
        [Required]
        [RegularExpression(@"^([A-ZĄĘŹŁŃŻŚĆÓ][a-zżźćąęłńśó]{2,40})([ -][A-ZĄĘŹŁŃŻŚĆÓ][a-zżźćąęłńśó]{2,40}){0,2}$", ErrorMessage = "Nazwa ulicy musi zaczynać się wielką literą")]
        public string ClinicAddress_Street { get; set; }
        [Display(Name = "Numer domu")]
        [Required]
        [RegularExpression(@"^[0-9]+[A-Z]?$", ErrorMessage = "Numer budynku musi być ciągiem cyfr, może kończyć się wielką literą")]
        public string ClinicAddress_StreetNumber { get; set; }
        [Display(Name = "Numer mieszkania")]
        [RegularExpression(@"^[0-9]+[A-Z]?$", ErrorMessage = "Numer lokalu musi być ciągiem cyfr, może kończyć się wielką literą")]
        public string ClinicAddress_HomeNumber { get; set; }
        [Display(Name = "Kod pocztowy")]
        [Required]
        [RegularExpression(@"^[0-9]{2}-[0-9]{3}$", ErrorMessage = "Kod pocztowy musi być formatu XX-XXX")]
        public string ClinicAddress_PostalCode { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}