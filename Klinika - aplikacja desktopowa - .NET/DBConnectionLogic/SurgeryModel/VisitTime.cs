//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Doktor_i_Pacjent_2017_XD_Pro.DBConnectionLogic.SurgeryModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class VisitTime
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
    
        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
