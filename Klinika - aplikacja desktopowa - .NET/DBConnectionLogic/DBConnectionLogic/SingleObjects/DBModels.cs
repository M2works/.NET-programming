using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doktor_i_Pacjent_2017_XD_Pro.DBConnectionLogic.SurgeryModel;

namespace Doktor_i_Pacjent_2017_XD_Pro.DBConnectionLogic
{

    public class DBModels : IDBModels
    {
        private SurgeryModelContainer sc;
        public DBModels(SurgeryModelContainer smc)
        {
            sc = smc;
        }
        #region singlePatientFunctions

        public virtual void AddPatient(string _name, string _surname, string _phoneNumber,
                                        string _peselNumber, Address _address)
        {
                Patient p = new Patient()
                {
                    Name = _name,
                    Surname = _surname,
                    Address = _address,
                    PhoneNumber = _phoneNumber,
                    PESELNumber = _peselNumber
                };

                sc.Patients.Add(p);
                sc.SaveChanges();
        }


        public virtual void DeletePatient(string _peselNumber)
        {
                var patient = (from p in sc.Patients
                               where p.PESELNumber == _peselNumber
                               select p).SingleOrDefault();

                sc.Entry(patient).Collection(x => x.VisitTime).Load();

                List<VisitTime> visits = patient.VisitTime.ToList();

                sc.Entry(patient).Collection(x => x.VisitTime).CurrentValue = null;

                for (int i = 0; i < visits.Count; i++)
                    sc.Timetables.Remove(visits[i]);

                sc.Patients.Remove(patient);
                sc.SaveChanges();
        }



        public virtual void EditPatient(string _currentPeselNumber, 
                    string _editedName, string _editedSurname, string _editedPhoneNumber,
                    string _editedPeselNumber, Address _editedAddress)
        {
                var patient = (from p in sc.Patients
                               where p.PESELNumber == _currentPeselNumber
                               select p).SingleOrDefault();

                patient.Name = _editedName;
                patient.Surname = _editedSurname;
                patient.Address = _editedAddress;
                patient.PESELNumber = _editedPeselNumber; //znak - czy można zrobić nieedytowalny pesel
                patient.PhoneNumber = _editedPhoneNumber;

                sc.SaveChanges();
        }

        #endregion

        #region listingPatients

        public virtual List<Patient> ListPatientsByNameAndSurname(string _name, string _surname, int _page, int _countElems)
        {
            List<Patient> pd = new List<Patient>();
            
            pd = (from p in sc.Patients
                    where p.Name.Contains(_name) && p.Surname.Contains(_surname)
                    orderby new { p.Name, p.PESELNumber }
                    select p).Skip(_page*_countElems).Take(_countElems).ToList();

            return pd;
        }

        public virtual List<Patient> ListPatientsBySurnameAndCity(string _city, string _surname, int _page, int _countElems)
        {
            List<Patient> pd = new List<Patient>();
            
            pd = (from p in sc.Patients
                    where p.Address.City.Contains(_city) && p.Surname.Contains(_surname)
                    orderby new { p.Name, p.PESELNumber }
                    select p).Skip(_page * _countElems).Take(_countElems).ToList();

            return pd;
        }

        public virtual List<Patient> ListPatientsByPESEL(string _pesel, int _page, int _countElems)
        {
            List<Patient> pd = new List<Patient>();
            
            pd = (from p in sc.Patients
                    where p.PESELNumber.Contains(_pesel)
                    orderby new { p.Name, p.PESELNumber }
                    select p).Skip(_page * _countElems).Take(_countElems).ToList();

            return pd;
        }
        public virtual List<Patient> ListPatientsByPESELandSurname(string _pesel, string _surname, int _page, int _countElems)
        {
            List<Patient> pd = new List<Patient>();

            pd = (from p in sc.Patients
                  where p.PESELNumber.Contains(_pesel) && p.Surname.Contains(_surname)
                  orderby new { p.Name, p.PESELNumber }
                  select p).Skip(_page * _countElems).Take(_countElems).ToList();

            return pd;
        }

        public virtual List<Patient> ListPatientsBySurname(string _surname, int _page, int _countElems)
        {
            List<Patient> pd = new List<Patient>();
            
            pd = (from p in sc.Patients
                    where p.Surname.Contains(_surname) == true
                    orderby new { p.Name, p.PESELNumber }
                    select p).Skip(_page * _countElems).Take(_countElems).ToList();

            return pd;
        }

        public virtual List<Patient> ListPatientsByNameSurnameAndCity(string _name, string _surname, string _city, int _page, int _countElems)
        {
            List<Patient> pd = new List<Patient>();
            
            pd = (from p in sc.Patients
                    where p.Name.Contains(_name) && p.Surname.Contains(_surname) && p.Address.City.Contains(_city)
                    orderby new { p.Name, p.PESELNumber }
                    select p).Skip(_page * _countElems).Take(_countElems).ToList();

            return pd;
        }

        #endregion

        #region listingDoctors

        public virtual List<Doctor> ListDoctorsBySpecNameAndSurname(Specialization spec, string _name, string _surname, int _page, int _countElems)
        {
            List<Doctor> pd = new List<Doctor>();
            
            pd = (from p in sc.Doctors
                    where p.Name.Contains(_name) && p.Surname.Contains(_surname) && p.Specialization == spec
                    orderby new { p.Name, p.LicenceNumber }
                    select p).Skip(_page * _countElems).Take(_countElems).ToList();

            for (int i = 0; i < pd.Count; i++)
                sc.Entry(pd[i]).Reference(x => x.Clinic).Load();

            return pd;
        }

        public virtual List<Doctor> ListDoctorsBySpecSurnameAndCity(Specialization spec, string _city, string _surname, int _page, int _countElems)
        {
            List<Doctor> pd = new List<Doctor>();

            pd = (from p in sc.Doctors
                    join c in sc.Clinics
                    on p.ClinicId equals c.Id
                    where p.Clinic.Address.City.Contains(_city) && p.Surname.Contains(_surname) && p.Specialization == spec
                    orderby new { p.Name, p.LicenceNumber }
                    select p).Skip(_page * _countElems).Take(_countElems).ToList();

            for (int i = 0; i < pd.Count; i++)
                sc.Entry(pd[i]).Reference(x => x.Clinic).Load();

            return pd;
        }

        public virtual List<Doctor> ListDoctorsBySpecAndCity(Specialization spec, string _city, int _page, int _countElems)
        {
            List<Doctor> pd = new List<Doctor>();
            
            pd = (from p in sc.Doctors
                    join c in sc.Clinics
                    on p.ClinicId equals c.Id
                    where p.Clinic.Address.City.Contains(_city) && p.Specialization == spec
                    orderby new { p.Name, p.LicenceNumber }
                    select p).Skip(_page * _countElems).Take(_countElems).ToList();

            for (int i = 0; i < pd.Count; i++)
                sc.Entry(pd[i]).Reference(x => x.Clinic).Load();

            return pd;
        }

        public virtual List<Doctor> ListDoctorsBySpecialization(Specialization _spec, int _page, int _countElems)
        {
            List<Doctor> pd = new List<Doctor>();
            
            pd = (from p in sc.Doctors
                    where p.Specialization == _spec
                    orderby new { p.Name, p.LicenceNumber }
                    select p).Skip(_page * _countElems).Take(_countElems).ToList();

            for (int i = 0; i < pd.Count; i++)
                sc.Entry(pd[i]).Reference(x => x.Clinic).Load();

            return pd;
        }

        public virtual List<Doctor> ListDoctorsBySpecSurname(Specialization spec, string _surname, int _page, int _countElems)
        {
            List<Doctor> pd = new List<Doctor>();
            
            pd = (from p in sc.Doctors
                    where p.Surname.Contains(_surname) && p.Specialization == spec
                    orderby new { p.Name, p.LicenceNumber }
                    select p).Skip(_page * _countElems).Take(_countElems).ToList();

            for (int i = 0; i < pd.Count; i++)
                sc.Entry(pd[i]).Reference(x => x.Clinic).Load();

            return pd;
        }

        public virtual List<Doctor> ListDoctorsBySurname(string _surname, int _page, int _countElems)
        {
            List<Doctor> pd = new List<Doctor>();

            pd = (from p in sc.Doctors
                  where p.Surname.Contains(_surname)
                  orderby new { p.Name, p.LicenceNumber }
                  select p).Skip(_page * _countElems).Take(_countElems).ToList();

            for (int i = 0; i < pd.Count; i++)
                sc.Entry(pd[i]).Reference(x => x.Clinic).Load();

            return pd;
        }

        public virtual List<Doctor> ListDoctorsByLicence(string _licenceNumber, int _page, int _countElems)
        {
            List<Doctor> pd = new List<Doctor>();

            pd = (from p in sc.Doctors
                  where p.LicenceNumber.Contains(_licenceNumber)
                  orderby new { p.Name, p.LicenceNumber }
                  select p).Skip(_page * _countElems).Take(_countElems).ToList();

            for (int i = 0; i < pd.Count; i++)
                sc.Entry(pd[i]).Reference(x => x.Clinic).Load();

            return pd;
        }
        public virtual List<Doctor> ListDoctorsByLicenceAndSurname(string _licenceNumber, string _surname, int _page, int _countElems)
        {
            List<Doctor> pd = new List<Doctor>();

            pd = (from p in sc.Doctors
                  where p.Surname.Contains(_surname) && p.LicenceNumber.Contains(_licenceNumber)
                  orderby new { p.Name, p.LicenceNumber }
                  select p).Skip(_page * _countElems).Take(_countElems).ToList();

            for (int i = 0; i < pd.Count; i++)
                sc.Entry(pd[i]).Reference(x => x.Clinic).Load();

            return pd;
        }

        public virtual List<Doctor> ListDoctorsBySpecNameSurnameAndCity(Specialization spec, string _name, string _surname, string _city, int _page, int _countElems)
        {
            List<Doctor> pd = new List<Doctor>();
            
            pd = (from p in sc.Doctors
                    join c in sc.Clinics
                    on p.ClinicId equals c.Id
                    where p.Name.Contains(_name) && p.Surname.Contains(_surname) && p.Clinic.Address.City.Contains(_city) && p.Specialization == spec
                    orderby new { p.Name, p.LicenceNumber }
                    select p).Skip(_page * _countElems).Take(_countElems).ToList();

            for (int i = 0; i < pd.Count; i++)
                sc.Entry(pd[i]).Reference(x => x.Clinic).Load();

            return pd;
        }

        #endregion

        #region nextStageFunctions

        public virtual void AddDoctor(string _name, string _surname, List<VisitTime> _visitTime, 
                                Clinic _clinic, Specialization _specialization, int? _visitPrice, string _licenceNumber)
        {
            Doctor dr = new Doctor()
            {
                Name = _name,
                Surname = _surname,
                VisitTime = _visitTime,
                Clinic = _clinic,
                Specialization = _specialization,
                VisitPrice = _visitPrice,
                LicenceNumber = _licenceNumber
            };

            sc.Doctors.Add(dr);
            sc.SaveChanges();
        }

        public virtual void AddClinic(string _name, Address _address, List<Doctor> _drs)
        {
            Clinic c = new Clinic()
            {
                Name = _name,
                Address=_address,
                Doctor=_drs //znak czy dodajemy ich już tu?
            };

            sc.Clinics.Add(c);
            sc.SaveChanges();
        }

        #endregion

        public virtual Address CreateAdress(string _country, string _city, string _street,
                                        int _streetNumber, int _homeNumber, int _postalCode )
        {
            Address a = new Address()
            {
                Country=_country,
                City=_city,
                Street=_street,
                StreetNumber=_streetNumber,
                HomeNumber=_homeNumber,
                PostalCode=_postalCode
            };

            return a;
        }

        #region listVisits

        public virtual List<VisitTime> ListVisitsOfPatientByPesel(string _pesel)
        {
            List<VisitTime> visits;

            visits = (from visit in sc.Timetables
                        where visit.Patient.PESELNumber == _pesel
                        orderby visit.Date
                        select visit).ToList();

            foreach (VisitTime vt in visits)
            {
                sc.Entry(vt).Reference(x => x.Doctor).Load();
                sc.Entry(vt.Doctor).Reference(x => x.Clinic).Load();
                sc.Entry(vt).Reference(x => x.Patient).Load();
            }

            return visits;
        }

        public virtual List<VisitTime> ListVisitsBySpec(DateTime dt, int _staringHour, int _endingHour, Specialization _specialization,
                           int _page, int _elemsCount)
        {
            List<VisitTime> visits;

            DateTime dt1 = dt.Date;
            dt1 = dt1.AddHours(_staringHour);
            DateTime dt2 = dt.Date;
            dt2 = dt2.AddHours(_endingHour);
            
            visits = (from visit in sc.Timetables
                        where visit.Doctor.Specialization == _specialization &&
                            visit.Date >= dt1 && visit.Date <= dt2
                        orderby visit.Date
                        select visit).Skip(_page * _elemsCount).Take(_elemsCount).ToList();

            foreach (VisitTime vt in visits)
            {
                sc.Entry(vt).Reference(x => x.Doctor).Load();
                sc.Entry(vt.Doctor).Reference(x => x.Clinic).Load();
                sc.Entry(vt).Reference(x => x.Patient).Load();
            }
                
            return visits;
        }

        public virtual List<VisitTime> ListVisitsBySpecCityAndDoctorName(DateTime dt, int _staringHour, int _endingHour,
                                        string _nameOfTheDoctor, string _city, Specialization _specialization, int _page, int _elemsCount)
        {
            List<VisitTime> visits;

            DateTime dt1 = dt.Date;
            dt1 = dt1.AddHours(_staringHour);
            DateTime dt2 = dt.Date;
            dt2 = dt2.AddHours(_endingHour);
            
            visits = (from visit in sc.Timetables
                        where visit.Doctor.Specialization == _specialization &&
                            visit.Date >= dt1 && visit.Date <= dt2 && 
                            visit.Doctor.Surname == _nameOfTheDoctor &&
                            visit.Doctor.Clinic.Address.City == _city
                        orderby visit.Date
                        select visit).Skip(_page*_elemsCount).Take(_elemsCount).ToList();

            foreach (VisitTime vt in visits)
            {
                sc.Entry(vt).Reference(x => x.Doctor).Load();
                sc.Entry(vt.Doctor).Reference(x => x.Clinic).Load();
                sc.Entry(vt).Reference(x => x.Patient).Load();
            }

            return visits;
        }

        public virtual List<VisitTime> ListVisitsBySpecAndDoctorName(DateTime dt, int _staringHour, int _endingHour, string _nameOfTheDoctor, Specialization _specialization, 
                                int _page, int _elemsCount)
        {
            List<VisitTime> visits;

            DateTime dt1 = dt.Date;
            dt1 = dt1.AddHours(_staringHour);
            DateTime dt2 = dt.Date;
            dt2 = dt2.AddHours(_endingHour);
            
            visits = (from visit in sc.Timetables
                        where (Specialization)visit.Doctor.Specialization == _specialization &&
                            visit.Date >= dt1 && visit.Date <= dt2 &&
                            visit.Doctor.Surname == _nameOfTheDoctor
                        orderby visit.Date
                        select visit).Skip(_page*_elemsCount).Take(_elemsCount).ToList();

            foreach (VisitTime vt in visits)
            {
                sc.Entry(vt).Reference(x => x.Doctor).Load();
                sc.Entry(vt.Doctor).Reference(x => x.Clinic).Load();
                sc.Entry(vt).Reference(x => x.Patient).Load();
            }

            return visits;
        }
        public List<VisitTime> ListVisitsBySpecAndCity(DateTime dt, int _staringHour, int _endingHour, Specialization _specialization, string _city,
                               int _page, int _elemsCount)
        {
            List<VisitTime> visits;

            DateTime dt1 = dt.Date;
            dt1 = dt1.AddHours(_staringHour);
            DateTime dt2 = dt.Date;
            dt2 = dt2.AddHours(_endingHour);

            visits = (from visit in sc.Timetables
                      where (Specialization)visit.Doctor.Specialization == _specialization &&
                          visit.Date >= dt1 && visit.Date <= dt2 &&
                          visit.Doctor.Clinic.Address.City == _city
                      orderby visit.Date
                      select visit).Skip(_page * _elemsCount).Take(_elemsCount).ToList();

            foreach (VisitTime vt in visits)
            {
                sc.Entry(vt).Reference(x => x.Doctor).Load();
                sc.Entry(vt.Doctor).Reference(x => x.Clinic).Load();
                sc.Entry(vt).Reference(x => x.Patient).Load();
            }

            return visits;
        }

        public List<VisitTime> ListVisitsByLicenceNumber(DateTime dt, int _staringHour, int _endingHour, string licenceNumber,
                               int _page, int _elemsCount)
        {
            List<VisitTime> visits;

            DateTime dt1 = dt.Date;
            dt1 = dt1.AddHours(_staringHour);
            DateTime dt2 = dt.Date;
            dt2 = dt2.AddHours(_endingHour);

            visits = (from visit in sc.Timetables
                      where visit.Date >= dt1 && visit.Date <= dt2 &&
                          visit.Doctor.LicenceNumber == licenceNumber
                      orderby visit.Date
                      select visit).Skip(_page * _elemsCount).Take(_elemsCount).ToList();

            foreach (VisitTime vt in visits)
            {
                sc.Entry(vt).Reference(x => x.Doctor).Load();
                sc.Entry(vt.Doctor).Reference(x => x.Clinic).Load();
                sc.Entry(vt).Reference(x => x.Patient).Load();
            }

            return visits;
        }
        #endregion

        #region singleVisitFunctions

        public virtual void DeleteVisitTime(int visitNumber)
        {
            VisitTime _vt = (from vt in sc.Timetables
                            where vt.Id == visitNumber
                            select vt).FirstOrDefault();

            sc.Timetables.Remove(_vt);
            sc.SaveChanges();
        }

        public virtual void AddVisitToDoctor(DateTime _dt, string _licenceNumber, string _peselNumber)
        {
            var patient = (from pat in sc.Patients
                            where pat.PESELNumber == _peselNumber
                            select pat).FirstOrDefault();

            var doctor = (from dr in sc.Doctors
                            where dr.LicenceNumber == _licenceNumber
                            select dr).FirstOrDefault();

            VisitTime vt = new VisitTime
            {
                Date = _dt,
                Doctor = doctor,
                Patient = patient
            };

            sc.Timetables.Add(vt);
            sc.SaveChanges();
        }
        public virtual int GetVisitNumber(DateTime _dt, string _licenceNumber, string _peselNumber)
        {
            int visitTimeNumber;
            
            var patientNumber = (from pat in sc.Patients
                            where pat.PESELNumber == _peselNumber
                            select pat.Id).FirstOrDefault();

            var doctorNumber = (from dr in sc.Doctors
                            where dr.LicenceNumber == _licenceNumber
                            select dr.Id).FirstOrDefault();

            visitTimeNumber = (from vt in sc.Timetables
                                where vt.PatientId == patientNumber && vt.DoctorId == doctorNumber
                                select vt.Id).FirstOrDefault();

            return visitTimeNumber;
        }

        #endregion

        #region FreeVisitGenerationMethods_byRemi
        public virtual List<VisitTime> ListVisitsAndDoctors_BySpec(DateTime dtstart, DateTime dtend, Specialization _specialization,
                           int _page, int _elemsCount, out List<Doctor> _doctors)
        {
            List<VisitTime> visits;


            _doctors = (from d in sc.Doctors
                       where d.Specialization == _specialization
                       orderby d.LicenceNumber
                       select d).Skip(_page * _elemsCount).Take(_elemsCount).ToList();

            foreach (Doctor d in _doctors)
                sc.Entry(d).Collection(x => x.VisitTime).Load();

            visits = _doctors.SelectMany(x => x.VisitTime).Where(vt => vt.Date >= dtstart && vt.Date <= dtend).ToList();

            foreach (VisitTime vt in visits)
            {
                sc.Entry(vt).Reference(x => x.Doctor).Load();
                sc.Entry(vt.Doctor).Reference(x => x.Clinic).Load();
                sc.Entry(vt).Reference(x => x.Patient).Load();
            }

            return visits;
        }

        public virtual List<VisitTime> ListVisitsAndDoctors_BySpecCityAndDoctorName(DateTime dtstart, DateTime dtend,
                                        string _nameOfTheDoctor, string _city, Specialization _specialization, int _page, int _elemsCount, out List<Doctor> _doctors)
        {
            List<VisitTime> visits;


            _doctors = (from d in sc.Doctors
                       where d.Specialization == _specialization &&
                            d.Clinic.Address.City == _city &&
                            d.Surname == _nameOfTheDoctor
                       orderby d.LicenceNumber
                       select d).Skip(_page * _elemsCount).Take(_elemsCount).ToList();

            foreach (Doctor d in _doctors)
                sc.Entry(d).Collection(x => x.VisitTime).Load();

            visits = _doctors.SelectMany(x => x.VisitTime).Where(vt => vt.Date >= dtstart && vt.Date <= dtend).ToList();
            return visits;
        }

        public virtual List<VisitTime> ListVisitsAndDoctors_BySpecAndDoctorName(DateTime dtstart, DateTime dtend, string _nameOfTheDoctor, Specialization _specialization,
                                int _page, int _elemsCount, out List<Doctor> _doctors)
        {
            List<VisitTime> visits;

            _doctors = (from d in sc.Doctors
                       where d.Specialization == _specialization &&
                            d.Surname == _nameOfTheDoctor
                       orderby d.LicenceNumber
                       select d).Skip(_page * _elemsCount).Take(_elemsCount).ToList();

            foreach (Doctor d in _doctors)
                sc.Entry(d).Collection(x => x.VisitTime).Load();
            
            visits = _doctors.SelectMany(x => x.VisitTime).Where(vt => vt.Date >= dtstart && vt.Date <= dtend).ToList();

            foreach (VisitTime vt in visits)
            {
                sc.Entry(vt).Reference(x => x.Doctor).Load();
                sc.Entry(vt.Doctor).Reference(x => x.Clinic).Load();
                sc.Entry(vt).Reference(x => x.Patient).Load();
            }

            return visits;
        }
        public List<VisitTime> ListVisitsAndDoctors_BySpecAndCity(DateTime dtstart, DateTime dtend, Specialization _specialization, string _city,
                               int _page, int _elemsCount, out List<Doctor> _doctors)
        {
            List<VisitTime> visits;

            _doctors = (from d in sc.Doctors
                       where d.Specialization == _specialization &&
                            d.Clinic.Address.City == _city
                       orderby d.LicenceNumber
                       select d).Skip(_page * _elemsCount).Take(_elemsCount).ToList();

            foreach (Doctor d in _doctors)
                sc.Entry(d).Collection(x => x.VisitTime).Load();

            visits = _doctors.SelectMany(x => x.VisitTime).Where(vt => vt.Date >= dtstart && vt.Date <= dtend).ToList();

            foreach (VisitTime vt in visits)
            {
                sc.Entry(vt).Reference(x => x.Doctor).Load();
                sc.Entry(vt.Doctor).Reference(x => x.Clinic).Load();
                sc.Entry(vt).Reference(x => x.Patient).Load();
            }

            return visits;
        }
        #endregion
    }
}
