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
    
    public partial class Admin
    {

        public int Id { get; set; }
        [Display(Name = "Login")]
        [Required]
        public string Login { get; set; }
        [Display(Name = "Haslo")]
        [Required]
        public string Password { get; set; }
    }
}
