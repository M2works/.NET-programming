using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doktor_i_Pacjent_2017_XD_Pro.DBConnectionLogic.SurgeryModel;

namespace Doktor_i_Pacjent_2017_XD_Pro.DBConnectionLogic
{
    public interface IDBModels
    {
        #region singlePatientFunctions

        void AddPatient(string _name, string _surname, string _phoneNumber,
                                        string _peselNumber, Address _address);

        void DeletePatient(string _peselNumber);

        void EditPatient(string _currentPeselNumber,
                    string _editedName, string _editedSurname, string _editedPhoneNumber,
                    string _editedPeselNumber, Address _editedAddress);

        #endregion
        #region listingPatients

        List<Patient> ListPatientsByNameAndSurname(string _name, string _surname, int _page, int _countElems);

        List<Patient> ListPatientsBySurnameAndCity(string _city, string _surname, int _page, int _countElems);
        List<Patient> ListPatientsByPESEL(string _pesel, int _page, int _countElems);
        List<Patient> ListPatientsBySurname(string _surname, int _page, int _countElems);
        List<Patient> ListPatientsByNameSurnameAndCity(string _name, string _surname, string _city, int _page, int _countElems);

        #endregion
        #region listingDoctors
        List<Doctor> ListDoctorsBySpecNameAndSurname(Specialization spec, string _name, string _surname, int _page, int _countElems);
        List<Doctor> ListDoctorsBySpecSurnameAndCity(Specialization spec, string _city, string _surname, int _page, int _countElems);
        List<Doctor> ListDoctorsBySpecAndCity(Specialization spec, string _city, int _page, int _countElems);
        List<Doctor> ListDoctorsBySpecialization(Specialization _spec, int _page, int _countElems);
        List<Doctor> ListDoctorsBySpecSurname(Specialization spec, string _surname, int _page, int _countElems);
        List<Doctor> ListDoctorsBySpecNameSurnameAndCity(Specialization spec, string _name, string _surname, string _city, int _page, int _countElems);
        #endregion
        #region nextStageFunctions

        void AddDoctor(string _name, string _surname, List<VisitTime> _visitTime,
                                Clinic _clinic, Specialization _specialization, int? _visitPrice, string _licenceNumber);

        #endregion
        #region addressFunctions
        Address CreateAdress(string _country, string _city, string _street,
                                        int _streetNumber, int _homeNumber, int _postalCode);
        #endregion
        #region listVisits
        List<VisitTime> ListVisitsOfPatientByPesel(string _pesel);
        List<VisitTime> ListVisitsBySpec(DateTime dt, int _staringHour, int _endingHour, Specialization _specialization,
                           int _page, int _elemsCount);
        List<VisitTime> ListVisitsBySpecCityAndDoctorName(DateTime dt, int _staringHour, int _endingHour,
                                        string _nameOfTheDoctor, string _city, Specialization _specialization, int _page, int _elemsCount);
        List<VisitTime> ListVisitsBySpecAndDoctorName(DateTime dt, int _staringHour, int _endingHour, string _nameOfTheDoctor, Specialization _specialization,
                                int _page, int _elemsCount);
        List<VisitTime> ListVisitsBySpecAndCity(DateTime dt, int _staringHour, int _endingHour, Specialization _specialization, string _city,
                                int _page, int _elemsCount);
        #endregion
        #region singleVisitFunctions

        void DeleteVisitTime(int visitNumber);
        void AddVisitToDoctor(DateTime _dt, string _licenceNumber, string _peselNumber);
        int  GetVisitNumber(DateTime _dt, string _licenceNumber, string _peselNumber);

        #endregion

        #region FreeVisitGenerationMethods_byRemi
        List<VisitTime> ListVisitsAndDoctors_BySpec(DateTime dtstart, DateTime dtend, Specialization _specialization,
                           int _page, int _elemsCount, out List<Doctor> _doctors);

        List<VisitTime> ListVisitsAndDoctors_BySpecCityAndDoctorName(DateTime dtstart, DateTime dtend,
                                        string _nameOfTheDoctor, string _city, Specialization _specialization, int _page, int _elemsCount, out List<Doctor> _doctors);

        List<VisitTime> ListVisitsAndDoctors_BySpecAndDoctorName(DateTime dtstart, DateTime dtend, string _nameOfTheDoctor, Specialization _specialization,
                                int _page, int _elemsCount, out List<Doctor> _doctors);
        List<VisitTime> ListVisitsAndDoctors_BySpecAndCity(DateTime dtstart, DateTime dtend, Specialization _specialization, string _city,
                               int _page, int _elemsCount, out List<Doctor> _doctors);
        #endregion
    }
}
