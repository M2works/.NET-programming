using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doktor_i_Pacjent_2017_XD_Pro;
using Doktor_i_Pacjent_2017_XD_Pro.DBConnectionLogic;
using System.Text.RegularExpressions;
using Doktor_i_Pacjent_2017_XD_Pro.DBConnectionLogic.SurgeryModel;

namespace Doktor_i_Pacjent_2017_XD_Pro.DataLogic
{
    public class Logic
    {
        private IDBModels dbConnection;
        public Logic(IDBModels dbConnection = null)
        {
            if (dbConnection == null)
            {
                SurgeryModelContainer smc = new SurgeryModelContainer();
                this.dbConnection = new DBModels(smc);
            }
            else
                this.dbConnection = dbConnection;
        }

        public IEnumerable<Patient> SearchPatient(string pesel, string surname, string name, string city, int page, int count)
        {
            List<Patient> patientsList = new List<Patient>();
            List<DBConnectionLogic.SurgeryModel.Patient> list = new List<DBConnectionLogic.SurgeryModel.Patient>();
            if (!String.IsNullOrEmpty(pesel) && Regex.IsMatch(pesel, "^[0-9]{3,11}$"))
            {
                foreach (var p in dbConnection.ListPatientsByPESEL(pesel, page, count))
                {
                    patientsList.Add(new Patient(p));
                }
                return patientsList;
            }
            else if (IsValidName(surname))
            {
                if (IsValidName(name))
                {
                    if (IsValidName(city))
                        list = dbConnection.ListPatientsByNameSurnameAndCity(name, surname, city, page, count);
                    else
                        list = dbConnection.ListPatientsByNameAndSurname(name, surname, page, count);
                }
                else if (IsValidName(city))
                {
                    list = dbConnection.ListPatientsBySurnameAndCity(city, surname, page, count);
                }
                else
                {
                    list = dbConnection.ListPatientsBySurname(surname, page, count);
                }
                foreach (var p in list)
                {
                    patientsList.Add(new Patient(p));
                }
                return patientsList;
            }
            return null;
        }

        public IEnumerable<Doctor> SearchDoctor(Specialization specialization, string surname, string name, string city, int page, int count)
        {
            List<Doctor> doctorsList = new List<Doctor>();
            List<DBConnectionLogic.SurgeryModel.Doctor> list = new List<DBConnectionLogic.SurgeryModel.Doctor>();
            if (IsValidName(surname))
            {
                if (IsValidName(name))
                {
                    if (IsValidName(city))
                        list = dbConnection.ListDoctorsBySpecNameSurnameAndCity(specialization, name, surname, city, page, count);
                    else
                        list = dbConnection.ListDoctorsBySpecNameAndSurname(specialization, name, surname, page, count);
                }
                else if (IsValidName(city))
                {
                    list = dbConnection.ListDoctorsBySpecSurnameAndCity(specialization, city, surname, page, count);
                }
                else
                {
                    list = dbConnection.ListDoctorsBySpecSurname(specialization, surname, page, count);
                }

            }
            else
            {
                list = dbConnection.ListDoctorsBySpecialization(specialization, page, count);
            }
            foreach (var dr in list)
            {
                doctorsList.Add(new Doctor(dr));
            }
            return doctorsList;
        }

        private bool IsValidName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;
            if (!Regex.IsMatch(name, "^([A-ZĄĘŹŁŃŻŚĆÓ][a-zżźćąęłńśó]{0,40}[- ]?)+$"))
                return false;
            return true;
        }

        private bool IsValidPesel(string pesel)
        {
            if (string.IsNullOrEmpty(pesel))
                return false;
            if (!Regex.IsMatch(pesel, "^[0-9]{11}$"))
                return false;
            return true;
        }

        private bool IsValidPhone(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return false;
            if (!Regex.IsMatch(phoneNumber, "^[+]?[0-9]{9,11}$"))
                return false;
            return true;
        }

        public bool CreatePatient(Patient patient)
        {
            if (ValidatePatient(patient))
            {
                return AddPatient(patient.Name, patient.Surname, patient.PhoneNumber, patient.PESELNumber, patient.Address);
            }
            else return false;
        }


        public bool AddDoctor(Doctor doctor)
        {
            if (ValidateDoctor(doctor))
            {
                dbConnection.AddDoctor(doctor.Name, doctor.Surname, null, null, doctor.Specialization, doctor.VisitPrice, doctor.LicenseNumber);
                return true;
            }
            else return false;
        }

        private bool ValidateAddress(Address a)
        {
            return IsValidName(a.City) && IsValidName(a.Country) && IsValidName(a.Street);
        }

        private bool ValidateDoctor(Doctor dr)
        {
            return IsValidName(dr.Name) && IsValidName(dr.Surname) && ValidateAddress(dr.Clinic.Address) && IsValidNumber(dr.LicenseNumber);
        }

        private bool IsValidNumber(string num)
        {
            if (string.IsNullOrEmpty(num))
                return false;
            if (!Regex.IsMatch(num, "^[0-9]{1,11}$"))
                return false;
            return true;
        }

        private bool ValidatePatient(Patient p)
        {
            return IsValidName(p.Name) && IsValidName(p.Surname) && IsValidPhone(p.PhoneNumber) && IsValidPesel(p.PESELNumber) && ValidateAddress(p.Address);
        }

        private bool AddPatient(string name, string surname, string phoneNumber, string PESEL, Address address)
        {
            dbConnection.AddPatient(name, surname, phoneNumber, PESEL, address);
            return true;
        }

        public bool DeletePatient(Patient p)
        {
            dbConnection.DeletePatient(p.PESELNumber);
            return true;
        }

        public IEnumerable<Visit> ShowPatientVisits(Patient p)
        {
            List<Visit> visitList = new List<Visit>();
            foreach (var v in dbConnection.ListVisitsOfPatientByPesel(p.PESELNumber))
            {
                visitList.Add(new Visit(new Doctor(v.Doctor), v.Date, p));
            }
            return visitList;
        }

        public bool UpdatePatient(Patient p)
        {
            if (ValidatePatient(p))
            {
                dbConnection.EditPatient(p.PESELNumber, p.Name, p.Surname, p.PhoneNumber, p.PESELNumber, p.Address);
                return true;
            }
            return false;
        }


        public bool AddVisit(DateTime dt, Patient p, Doctor dr)
        {
            dbConnection.AddVisitToDoctor(dt, dr.LicenseNumber, p.PESELNumber);
            return true;
        }

        public class VisitComparer : IComparer<Visit>
        {
            public int Compare(Visit x, Visit y)
            {
                if (x.DT > y.DT) return 1;
                if (x.DT < y.DT) return -1;
                if (x.Doctor.LicenseNumber != y.Doctor.LicenseNumber) return 1;
                return 0;
            }
        }


        public IEnumerable<Visit> ShowVisits(DateTime _dt, string startHour, string endHour, string doctorName, string city, Specialization specialization, bool freeOnly, int _page, int count)
        {
            const int N = 500;
            int page = 0;

            SortedSet<Visit> visits = new SortedSet<Visit>(new VisitComparer());
            List<VisitTime> list = new List<VisitTime>();
            List<DBConnectionLogic.SurgeryModel.Doctor> doctors;
            List<Doctor> myDoctors = new List<Doctor>();
            int startTime;
            int endTime;
            int.TryParse(startHour.Split(':')[0], out startTime);
            int.TryParse(endHour.Split(':')[0], out endTime);
            int day = _dt.Day;
            DateTime dtstart = _dt.AddHours(startTime);
            DateTime dtend = _dt.AddHours(endTime);
            DateTime dt = _dt.AddHours(startTime);

            page = 0;
            while (true)
            {
                for (DateTime d = dtstart; d < dtend; d = d.AddHours(1))
                {

                    while (true)
                    {

                        if (IsValidName(doctorName))
                        {
                            if (IsValidName(city))
                                list = dbConnection.ListVisitsAndDoctors_BySpecCityAndDoctorName(dtstart, dtend, doctorName, city, specialization, page, N, out doctors); //warning
                            else
                                list = dbConnection.ListVisitsAndDoctors_BySpecAndDoctorName(dtstart, dtend, doctorName, specialization, page, N, out doctors); //
                        }
                        else if (IsValidName(city))
                            list = dbConnection.ListVisitsAndDoctors_BySpecAndCity(dtstart, dtend, specialization, city, page, N, out doctors);
                        else
                            list = dbConnection.ListVisitsAndDoctors_BySpec(dtstart, dtend, specialization, page, N, out doctors);
                        if (page == 0 && doctors.Count == 0)
                        {
                            return null;
                        }
                        if (doctors.Count == 0)
                        {
                            page = 0;
                            break;
                        }
                        for (int j = 0; j < 4; ++j, dt = dt.AddMinutes(15))
                        {
                            foreach (var dr in doctors)
                            {
                                Doctor D = new Doctor(dr);
                                if (!list.Exists(v => v.Doctor.LicenceNumber == dr.LicenceNumber && v.Date == dt))
                                    visits.Add(new Visit(D, dt));
                                else if(freeOnly == false)
                                    visits.Add(new Visit(list.Find(v => v.Doctor.LicenceNumber == dr.LicenceNumber && v.Date == dt)));
                                if (visits.Count == count)
                                    return visits;
                            }
                        }
                    }
                    page++;
                }
                dtstart = dtstart.AddDays(1);
                dtend = dtend.AddDays(1);
            }
        }
        

        public bool DeleteVisit(DateTime dt, Doctor dr, Patient p)
        {
            int visitNumber = dbConnection.GetVisitNumber(dt, dr.LicenseNumber, p.PESELNumber);
            dbConnection.DeleteVisitTime(visitNumber);
            return true;
        }
    }
}

