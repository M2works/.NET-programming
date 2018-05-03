using Doktor_i_Pacjent_2017_XD_Pro.DBConnectionLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doktor_i_Pacjent_2017_XD_Pro.DBConnectionLogic.SurgeryModel;

namespace Doktor_i_Pacjent_2017_XD_Pro.DataLogic.Tests
{
    public class TestDBConnector : IDBModels
    {
        public List<DBConnectionLogic.SurgeryModel.Patient> __patients = new List<DBConnectionLogic.SurgeryModel.Patient>();
        public  List<DBConnectionLogic.SurgeryModel.Doctor> __doctors = new List<DBConnectionLogic.SurgeryModel.Doctor>();
        public List<VisitTime> __visits = new List<VisitTime>();

        public void AddDoctor(string _name, string _surname, List<VisitTime> _visitTime, DBConnectionLogic.SurgeryModel.Clinic _clinic, Specialization _specialization, int? _visitPrice, string _licenceNumber)
        {
            __doctors.Add(new DBConnectionLogic.SurgeryModel.Doctor() { Name = _name, Surname = _surname, VisitTime = _visitTime, Clinic = _clinic, Specialization = _specialization, VisitPrice = _visitPrice, LicenceNumber = _licenceNumber });
        }

        public void AddPatient(string _name, string _surname, string _phoneNumber, string _peselNumber, Address _address)
        {
            __patients.Add(new DBConnectionLogic.SurgeryModel.Patient() { Name = _name, Surname = _surname, PhoneNumber = _phoneNumber, PESELNumber=_peselNumber, Address = _address });
        }

        public void AddVisitToDoctor(DateTime _dt, string _licenceNumber, string _peselNumber)
        {
            __visits.Add(new VisitTime() { Date = _dt, Doctor = __doctors.Find(d => d.LicenceNumber == _licenceNumber), Patient = __patients.Find(p => p.PESELNumber == _peselNumber) });
        }

        public Address CreateAdress(string _country, string _city, string _street, int _streetNumber, int _homeNumber, int _postalCode)
        {
            return new Address() { Country = _country, City = _city, Street = _street, StreetNumber = _streetNumber, HomeNumber = _homeNumber, PostalCode = _postalCode };
        }

        public void DeletePatient(string _peselNumber)
        {
            __patients.RemoveAll(p => p.PESELNumber == _peselNumber);
        }

        public void DeleteVisitTime(int visitNumber)
        {
            __visits.RemoveAt(visitNumber);
        }

        public void EditPatient(string _currentPeselNumber, string _editedName, string _editedSurname, string _editedPhoneNumber, string _editedPeselNumber, Address _editedAddress)
        {
            DBConnectionLogic.SurgeryModel.Patient p = __patients.Find(x => x.PESELNumber == _currentPeselNumber);
            p.Name = _editedName;
            p.Surname = _editedSurname;
            p.PhoneNumber = _editedPhoneNumber;
            p.Address = _editedAddress;
        }

        public int GetVisitNumber(DateTime _dt, string _licenceNumber, string _peselNumber)
        {
            return __visits.FindIndex(v => v.Date == _dt && v.Doctor.LicenceNumber == _licenceNumber && v.Patient.PESELNumber == _peselNumber);
        }

        public List<DBConnectionLogic.SurgeryModel.Doctor> ListDoctorsBySpecAndCity(Specialization spec, string _city, int _page, int _countElems)
        {
            return __doctors.FindAll(d => d.Specialization == spec && d.Clinic.Address.City.StartsWith(_city)).GetRange(_page * _countElems, _countElems);
        }

        public List<DBConnectionLogic.SurgeryModel.Doctor> ListDoctorsBySpecialization(Specialization _spec, int _page, int _countElems)
        {
            return __doctors.FindAll(d => d.Specialization == _spec).GetRange(_page * _countElems, _countElems);
        }

        public List<DBConnectionLogic.SurgeryModel.Doctor> ListDoctorsBySpecNameAndSurname(Specialization spec, string _name, string _surname, int _page, int _countElems)
        {
            return __doctors.FindAll(d => d.Specialization == spec && d.Name.StartsWith(_name) && d.Surname.StartsWith(_surname)).GetRange(_page * _countElems, _countElems);
        }

        public List<DBConnectionLogic.SurgeryModel.Doctor> ListDoctorsBySpecNameSurnameAndCity(Specialization spec, string _name, string _surname, string _city, int _page, int _countElems)
        {
            return __doctors.FindAll(d => d.Specialization == spec && d.Surname.StartsWith(_surname) && d.Name.StartsWith(_name) && d.Clinic.Address.City.StartsWith(_city)).GetRange(_page * _countElems, _countElems);
        }

        public List<DBConnectionLogic.SurgeryModel.Doctor> ListDoctorsBySpecSurname(Specialization spec, string _surname, int _page, int _countElems)
        {
            return __doctors.FindAll(d => d.Specialization == spec && d.Surname.StartsWith(_surname)).GetRange(_page * _countElems, _countElems);
        }

        public List<DBConnectionLogic.SurgeryModel.Doctor> ListDoctorsBySpecSurnameAndCity(Specialization spec, string _city, string _surname, int _page, int _countElems)
        {
            return __doctors.FindAll(d => d.Specialization == spec && d.Surname.StartsWith(_surname) && d.Clinic.Address.City.StartsWith(_city)).GetRange(_page * _countElems, _countElems);
        }

        public List<DBConnectionLogic.SurgeryModel.Patient> ListPatientsByNameAndSurname(string _name, string _surname, int _page, int _countElems)
        {
            return __patients.FindAll(p => p.Name.StartsWith(_name) && p.Surname.StartsWith(_surname)).GetRange(_page * _countElems, _countElems);
        }

        public List<DBConnectionLogic.SurgeryModel.Patient> ListPatientsByNameSurnameAndCity(string _name, string _surname, string _city, int _page, int _countElems)
        {
            return __patients.FindAll(p => p.Name.StartsWith(_name) && p.Surname.StartsWith(_surname) && p.Address.City.StartsWith(_city)).GetRange(_page * _countElems, _countElems);
        }

        public List<DBConnectionLogic.SurgeryModel.Patient> ListPatientsByPESEL(string _pesel, int _page, int _countElems)
        {
            return __patients.FindAll(p => p.PESELNumber.StartsWith(_pesel)).GetRange(_page * _countElems, _countElems);
        }

        public List<DBConnectionLogic.SurgeryModel.Patient> ListPatientsBySurname(string _surname, int _page, int _countElems)
        {
            return __patients.FindAll(p => p.Surname.StartsWith(_surname)).GetRange(_page * _countElems, _countElems);
        }

        public List<DBConnectionLogic.SurgeryModel.Patient> ListPatientsBySurnameAndCity(string _city, string _surname, int _page, int _countElems)
        {
            return __patients.FindAll(p => p.Surname.StartsWith(_surname) && p.Address.City.StartsWith(_city)).GetRange(_page * _countElems, _countElems);
        }

        public List<VisitTime> ListVisitsBySpec(DateTime dt, int _staringHour, int _endingHour, Specialization _specialization, int _page, int _elemsCount)
        {
            return __visits.FindAll(v => v.Date.Date == dt.Date && v.Date.Hour >= _staringHour && v.Date.Hour <= _endingHour && v.Doctor.Specialization == _specialization).GetRange(_page * _elemsCount, _elemsCount);
        }

        public List<VisitTime> ListVisitsBySpecAndCity(DateTime dt, int _staringHour, int _endingHour, Specialization _specialization, string _city, int _page, int _elemsCount)
        {
            return __visits.FindAll(v => v.Date.Date == dt.Date && v.Date.Hour >= _staringHour && v.Date.Hour <= _endingHour && v.Doctor.Specialization == _specialization && v.Doctor.Clinic.Address.City.StartsWith(_city)).GetRange(_page * _elemsCount, _elemsCount);
        }

        public List<VisitTime> ListVisitsBySpecAndDoctorName(DateTime dt, int _staringHour, int _endingHour, string _nameOfTheDoctor, Specialization _specialization, int _page, int _elemsCount)
        {
            return __visits.FindAll(v => v.Date.Date == dt.Date && v.Date.Hour >= _staringHour && v.Date.Hour <= _endingHour && v.Doctor.Specialization == _specialization && v.Doctor.Surname.StartsWith(_nameOfTheDoctor)).GetRange(_page * _elemsCount, _elemsCount);
        }

        public List<VisitTime> ListVisitsBySpecCityAndDoctorName(DateTime dt, int _staringHour, int _endingHour, string _nameOfTheDoctor, string _city, Specialization _specialization, int _page, int _elemsCount)
        {
            return __visits.FindAll(v => v.Date.Date == dt.Date && v.Date.Hour >= _staringHour && v.Date.Hour <= _endingHour && v.Doctor.Specialization == _specialization && v.Doctor.Surname.StartsWith(_nameOfTheDoctor) && v.Doctor.Clinic.Address.City.StartsWith(_city)).GetRange(_page * _elemsCount, _elemsCount);
        }

        public List<VisitTime> ListVisitsOfPatientByPesel(string _pesel)
        {
            return __visits.FindAll(v => v.Patient.PESELNumber.StartsWith(_pesel));
        }



        public List<VisitTime> ListVisitsAndDoctors_BySpec(DateTime dtstart, DateTime dtend, Specialization _specialization, int _page, int _elemsCount, out List<DBConnectionLogic.SurgeryModel.Doctor> _doctors)
        {
            List<VisitTime> visits;
            List<DBConnectionLogic.SurgeryModel.Doctor> doctors;


            doctors = (from d in this.__doctors
                       where d.Specialization == _specialization
                       orderby d.LicenceNumber
                       select d).Skip(_page * _elemsCount).Take(_elemsCount).ToList();

            visits = (from visit in __visits
                      where
                      visit.Date >= dtstart &&
                      visit.Date <= dtend &&
                      doctors.Contains(visit.Doctor)
                      orderby visit.Date
                      select visit).ToList();
            _doctors = doctors;
            return visits;
        }

        public List<VisitTime> ListVisitsAndDoctors_BySpecCityAndDoctorName(DateTime dtstart, DateTime dtend, string _nameOfTheDoctor, string _city, Specialization _specialization, int _page, int _elemsCount, out List<DBConnectionLogic.SurgeryModel.Doctor> _doctors)
        {
            List<VisitTime> visits;
            List<DBConnectionLogic.SurgeryModel.Doctor> doctors;


            doctors = (from d in this.__doctors
                       where d.Specialization == _specialization &&
                       d.Clinic.Address.City.StartsWith(_city) &&
                       d.Surname.StartsWith(_nameOfTheDoctor)
                       orderby d.LicenceNumber
                       select d).Skip(_page * _elemsCount).Take(_elemsCount).ToList();

            visits = (from visit in __visits
                      where
                      visit.Date >= dtstart &&
                      visit.Date <= dtend &&
                      doctors.Contains(visit.Doctor)
                      orderby visit.Date
                      select visit).ToList();
            _doctors = doctors;
            return visits;
        }

        public List<VisitTime> ListVisitsAndDoctors_BySpecAndDoctorName(DateTime dtstart, DateTime dtend, string _nameOfTheDoctor, Specialization _specialization, int _page, int _elemsCount, out List<DBConnectionLogic.SurgeryModel.Doctor> _doctors)
        {
            List<VisitTime> visits;
            List<DBConnectionLogic.SurgeryModel.Doctor> doctors;


            doctors = (from d in this.__doctors
                       where d.Specialization == _specialization &&
                       d.Surname.StartsWith(_nameOfTheDoctor)
                       orderby d.LicenceNumber
                       select d).Skip(_page * _elemsCount).Take(_elemsCount).ToList();

            visits = (from visit in __visits
                      where
                      visit.Date >= dtstart &&
                      visit.Date <= dtend &&
                      doctors.Contains(visit.Doctor)
                      orderby visit.Date
                      select visit).ToList();
            _doctors = doctors;
            return visits;
        }

        public List<VisitTime> ListVisitsAndDoctors_BySpecAndCity(DateTime dtstart, DateTime dtend, Specialization _specialization, string _city, int _page, int _elemsCount, out List<DBConnectionLogic.SurgeryModel.Doctor> _doctors)
        {
            List<VisitTime> visits;
            List<DBConnectionLogic.SurgeryModel.Doctor> doctors;


            doctors = (from d in this.__doctors
                       where d.Specialization == _specialization &&
                       d.Clinic.Address.City.StartsWith(_city)
                       orderby d.LicenceNumber
                       select d).Skip(_page * _elemsCount).Take(_elemsCount).ToList();

            visits = (from visit in __visits
                      where
                      visit.Date >= dtstart &&
                      visit.Date <= dtend &&
                      doctors.Contains(visit.Doctor)
                      orderby visit.Date
                      select visit).ToList();
            _doctors = doctors;
            return visits;
        }
    }
}
