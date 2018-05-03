using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class DoctorVisitsManager
    {
        public DoctorVisitsManager(Doctor d, List<VisitTime> visits)
        {
            Doctor = d;
            Visits = visits;
        }
        public Doctor Doctor { get; set; }
        public List<VisitTime> Visits { get; set; }
    }
}