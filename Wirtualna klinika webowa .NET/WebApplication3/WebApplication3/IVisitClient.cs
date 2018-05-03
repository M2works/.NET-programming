using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication3
{
    public interface IVisitClient
    {
        IEnumerable<VisitTime> GetDoctorVisits(string licenseNumber);
    }
    public class VisitClient : IVisitClient
    {
        public IEnumerable<VisitTime> GetDoctorVisits(string licenseNumber)
        {
            IEnumerable<VisitTime> visits = null;


            return visits;
        }
    }
}