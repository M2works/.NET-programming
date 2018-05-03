using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doktor_i_Pacjent_2017_XD_Pro.DBConnectionLogic.SurgeryModel;

namespace Doktor_i_Pacjent_2017_XD_Pro.DataLogic
{
    public class ExampleData : IDisposable
    {
        private static Random R;
        public ExampleData(int? seed = null)
        {
            if (seed == null)
                R = new Random();
            else
                R = new Random(seed.Value);
        }

        public string[] ExampleNames()
        {
            return new string[] { "Jan", "Adam", "Maria", "Anna", "Stanisław", "Eustachy", "Katarzyna", "Kamil", "Zbigniew", "Lucjan", "Klaudia" };
        }
        public string[] ExampleSurnames()
        {
            return new string[] { "Stojke", "Opalach", "Momot", "Pietrus", "Srokosz", "Piesio", "Wołosz", "Czuchra", "Religa", "Tusk", "Potter" };
        }
        public string ExampleLicenseNumber()
        {
            return R.Next(100000, 999999).ToString();
        }
        public string ExamplePhoneNumber()
        {
            return R.Next(100000000, 999999999).ToString();
        }
        public string ExamplePeselNumber()
        {
            return R.Next(100000, 999999).ToString() + R.Next(10000, 99999);
        }
        public int ExamplePostalCode()
        {
            return R.Next(10000, 99999);
        }
        public DateTime ExampleTime()
        {
            return new DateTime(R.Next(2017, 2019), R.Next(1, 12), R.Next(1, 25), R.Next(8, 17), 15 * R.Next(0, 3), 0);
        }
        public bool ExampleTrue(double chance)
        {
            return R.NextDouble() < chance;
        }
        public string[] ExampleCities()
        {
            return new string[] { "Warszwa", "Łódź", "Lublin", "Kraków", "Łomża", "Żywiec", "Warka", "Zielona Góra" };
        }
        public string[] ExampleStreets()
        {
            return new string[] { "Długa", "Warszawska", "Autostrada", "Jana Pawła II", "Lwowska", "Parkowa", "Słoneczna", "Waryńskiego", "Bliska" };
        }
        public IEnumerable<Patient> ExamplePatient(int n)
        {
            Queue<Patient> q = new Queue<Patient>();
            for (int i = 0; i < n; i++)
                q.Enqueue(ExamplePatient());
            return q;
        }
        public Patient ExamplePatient()
        {
            Patient p = new Patient();
            p.Name = ExampleNames()[R.Next(ExampleNames().Length)];
            p.Surname = ExampleSurnames()[R.Next(ExampleSurnames().Length)];
            p.PhoneNumber = ExamplePhoneNumber();
            p.PESELNumber = ExamplePeselNumber();
            p.Address = ExampleAddress();
            return p;
        }
        public IEnumerable<Doctor> ExampleDoctor(int n)
        {
            Queue<Doctor> q = new Queue<Doctor>();
            for (int i = 0; i < n; i++)
                q.Enqueue(ExampleDoctor());
            return q;
        }
        public Doctor ExampleDoctor()
        {
            Doctor d = new Doctor();
            d.Name = ExampleNames()[R.Next(ExampleNames().Length)];
            d.Surname = ExampleSurnames()[R.Next(ExampleSurnames().Length)];
            d.Clinic = new Clinic(ExampleAddress(), "Name");
            d.Specialization = (Specialization)R.Next(0, 7);
            d.VisitPrice = 10 * R.Next(5, 25);
            d.LicenseNumber = ExampleLicenseNumber();
            return d;
        }
        public IEnumerable<Address> ExampleAddress(int n)
        {
            Queue<Address> q = new Queue<Address>();
            for (int i = 0; i < n; i++)
                q.Enqueue(ExampleAddress());
            return q;
        }
        public Address ExampleAddress()
        {
            Address a = new Address();
            a.Country = "Poland";
            a.City = ExampleCities()[R.Next(ExampleCities().Length)];
            a.Street = ExampleStreets()[R.Next(ExampleStreets().Length)];
            a.StreetNumber = R.Next(1, 128);
            a.HomeNumber = R.Next(1, 30);
            a.PostalCode = ExamplePostalCode();
            return a;
        }
        public Visit ExampleVisit()
        {
            return new Visit(ExampleDoctor(), ExampleTime(), ExampleTrue(0.35) ? ExamplePatient() : null);
        }
        public Visit ExampleVisit(Patient p)
        {
            return new Visit(ExampleDoctor(), ExampleTime(), p);
        }
        public Visit ExampleVisit(Doctor d, Patient p = null)
        {
            return new Visit(d, ExampleTime(), p);
        }
        public Visit ExampleVisit(DateTime dt, Doctor d = null, Patient p = null)
        {
            return new Visit(d, dt, p);
        }
        public IEnumerable<Visit> ExampleVisit(int n)
        {
            Queue<Visit> q = new Queue<Visit>();
            for (int i = 0; i < n; i++)
                q.Enqueue(ExampleVisit());
            return q;
        }

        public void Dispose()   {  }
    }
}
