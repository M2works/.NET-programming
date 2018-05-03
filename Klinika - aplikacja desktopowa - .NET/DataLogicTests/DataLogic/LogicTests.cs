using Microsoft.VisualStudio.TestTools.UnitTesting;
using Doktor_i_Pacjent_2017_XD_Pro.DataLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doktor_i_Pacjent_2017_XD_Pro.DataLogic.Tests;
using Doktor_i_Pacjent_2017_XD_Pro.DBConnectionLogic;

namespace Doktor_i_Pacjent_2017_XD_Pro.DataLogic.Tests
{
    [TestClass()]
    public class LogicTests
    {
        [TestMethod()]
        public void DeletePatientTest()
        {
            TestDBConnector fakeDBModel = new TestDBConnector();
            Logic testLogic = new Logic(fakeDBModel);
            using (ExampleData data = new ExampleData(0))
            {
                Patient p = data.ExamplePatient();
                testLogic.CreatePatient(p);

                bool ret = testLogic.DeletePatient(p);

                Assert.AreEqual(0, fakeDBModel.__patients.Count);
            }
        }

        [TestMethod()]
        public void SearchPatientTest()
        {
            IDBModels fakeDBModel = new TestDBConnector();
            Logic testLogic = new Logic(fakeDBModel);
            using (ExampleData data = new ExampleData(0))
            {
                Patient p1 = data.ExamplePatient();
                Patient p2 = data.ExamplePatient();
                testLogic.CreatePatient(p1);
                testLogic.CreatePatient(p2);

                List<Patient> list = (List<Patient>)testLogic.SearchPatient(p1.PESELNumber, p1.Surname, p1.Name, p1.Address.City, 0, 1);

                Assert.AreEqual(p1.PESELNumber, list[0].PESELNumber);
            }
        }

        [TestMethod()]
        public void SearchPatientByPeselTest()
        {
            IDBModels fakeDBModel = new TestDBConnector();
            Logic testLogic = new Logic(fakeDBModel);
            using (ExampleData data = new ExampleData(0))
            {
                Patient p1 = data.ExamplePatient();
                Patient p2 = data.ExamplePatient();
                testLogic.CreatePatient(p1);
                testLogic.CreatePatient(p2);

                List<Patient> list = (List<Patient>)testLogic.SearchPatient(p1.PESELNumber, "", "", "", 0, 1);

                Assert.AreEqual(p1.PESELNumber, list[0].PESELNumber);
            }
        }

        [TestMethod()]
        public void InvalidArgumentsSearchPatientTest()
        {
            IDBModels fakeDBModel = new TestDBConnector();
            Logic testLogic = new Logic(fakeDBModel);
            using (ExampleData data = new ExampleData(0))
            {
                Patient p1 = data.ExamplePatient();
                Patient p2 = data.ExamplePatient();
                testLogic.CreatePatient(p1);
                testLogic.CreatePatient(p2);

                List<Patient> list = (List<Patient>)testLogic.SearchPatient("", "", "", "", 0, 1);

                Assert.AreEqual(null, list);
            }
        }

        [TestMethod()]
        public void SearchPatientBySurnameNameAndCityTest()
        {
            IDBModels fakeDBModel = new TestDBConnector();
            Logic testLogic = new Logic(fakeDBModel);
            using (ExampleData data = new ExampleData(0))
            {
                Patient p1 = data.ExamplePatient();
                Patient p2 = data.ExamplePatient();
                testLogic.CreatePatient(p1);
                testLogic.CreatePatient(p2);

                List<Patient> list = (List<Patient>)testLogic.SearchPatient("", p1.Surname, p1.Name, p1.Address.City, 0, 1);

                Assert.AreEqual(p1.PESELNumber, list[0].PESELNumber);
            }
        }


        [TestMethod()]
        public void SearchPatientBySurnameAndCityTest()
        {
            IDBModels fakeDBModel = new TestDBConnector();
            Logic testLogic = new Logic(fakeDBModel);
            using (ExampleData data = new ExampleData(0))
            {
                Patient p1 = data.ExamplePatient();
                Patient p2 = data.ExamplePatient();
                testLogic.CreatePatient(p1);
                testLogic.CreatePatient(p2);

                List<Patient> list = (List<Patient>)testLogic.SearchPatient("", p1.Surname, "", p1.Address.City, 0, 1);

                Assert.AreEqual(p1.PESELNumber, list[0].PESELNumber);
            }
        }

        [TestMethod()]
        public void SearchPatientBySurnameAndNameTest()
        {
            IDBModels fakeDBModel = new TestDBConnector();
            Logic testLogic = new Logic(fakeDBModel);
            using (ExampleData data = new ExampleData(0))
            {
                Patient p1 = data.ExamplePatient();
                Patient p2 = data.ExamplePatient();
                testLogic.CreatePatient(p1);
                testLogic.CreatePatient(p2);

                List<Patient> list = (List<Patient>)testLogic.SearchPatient("", p1.Surname, p1.Name, "", 0, 1);

                Assert.AreEqual(p1.PESELNumber, list[0].PESELNumber);
            }
        }


        [TestMethod()]
        public void CreatePatientTest()
        {
            TestDBConnector fakeDBModel = new TestDBConnector();
            Logic testLogic = new Logic(fakeDBModel);
            using (ExampleData data = new ExampleData(0))
            {
                Patient p = data.ExamplePatient();
                testLogic.CreatePatient(p);

                Assert.AreEqual(1, fakeDBModel.__patients.Count);
            }
        }

        [TestMethod()]
        public void CreateInvalidPatientTest()
        {
            TestDBConnector fakeDBModel = new TestDBConnector();
            Logic testLogic = new Logic(fakeDBModel);
            using (ExampleData data = new ExampleData(0))
            {
                Patient p = data.ExamplePatient();
                p.Name = "ja";
                bool ret = testLogic.CreatePatient(p);

                Assert.AreEqual(false, ret);
            }
        }

        [TestMethod()]
        public void UpdatePatientTest()
        {
            TestDBConnector fakeDBModel = new TestDBConnector();
            Logic testLogic = new Logic(fakeDBModel);
            using (ExampleData data = new ExampleData(0))
            {
                Patient p = data.ExamplePatient();
                testLogic.CreatePatient(p);
                p.Name = "Ania";
                bool ret = testLogic.UpdatePatient(p);
                Assert.AreEqual(true, ret);
                Assert.AreEqual("Ania", fakeDBModel.__patients[0].Name);
            }
        }

        [TestMethod()]
        public void InvalidUpdatePatientTest()
        {
            TestDBConnector fakeDBModel = new TestDBConnector();
            Logic testLogic = new Logic(fakeDBModel);
            using (ExampleData data = new ExampleData(0))
            {
                Patient p = data.ExamplePatient();
                testLogic.CreatePatient(p);
                String oldName = p.Name;
                p.Name = "ania";
                bool ret = testLogic.UpdatePatient(p);
                Assert.AreEqual(false, ret);
                Assert.AreEqual(oldName, fakeDBModel.__patients[0].Name);
            }
        }

        [TestMethod()]
        public void ShowPatientVisitsTest()
        {
            TestDBConnector fakeDBModel = new TestDBConnector();
            Logic testLogic = new Logic(fakeDBModel);
            using (ExampleData data = new ExampleData(0))
            {
                Patient p = data.ExamplePatient();
                Doctor dr = data.ExampleDoctor();
                DateTime dt = data.ExampleTime();
                testLogic.CreatePatient(p);
                testLogic.AddDoctor(dr);
                testLogic.AddVisit(dt, p, dr);

                List<Visit> list = (List<Visit>)testLogic.ShowPatientVisits(p);

                Assert.AreEqual(p.PESELNumber, list[0].Patient.PESELNumber);
                Assert.AreEqual(dr.LicenseNumber, list[0].Doctor.LicenseNumber);
                Assert.AreEqual(dt, list[0].DT);
            }
        }

        [TestMethod()]
        public void AddVisitTest()
        {
            TestDBConnector fakeDBModel = new TestDBConnector();
            Logic testLogic = new Logic(fakeDBModel);
            using (ExampleData data = new ExampleData(0))
            {
                Patient p = data.ExamplePatient();
                Doctor dr = data.ExampleDoctor();
                DateTime dt = data.ExampleTime();

                testLogic.AddVisit(dt, p, dr);


                Assert.AreEqual(1, fakeDBModel.__visits.Count);
            }
        }

        [TestMethod()]
        public void ShowVisitsTestNull()
        {
            TestDBConnector fakeDBModel = new TestDBConnector();
            Logic testLogic = new Logic(fakeDBModel);
            using (ExampleData data = new ExampleData(0))
            {
                ;
                Doctor dr = data.ExampleDoctor();
                DateTime dt = data.ExampleTime();

                var list = testLogic.ShowVisits(dt, 8.ToString(), 20.ToString(), dr.Name, "", dr.Specialization, true, 0, 0);


                Assert.AreEqual(null, list);
            }
        }


        [TestMethod()]
        public void ShowVisitsTestAnyVisit()
        {
            TestDBConnector fakeDBModel = new TestDBConnector();
            Logic testLogic = new Logic(fakeDBModel);
            using (ExampleData data = new ExampleData(0))
            {

                Doctor dr = data.ExampleDoctor();
                testLogic.AddDoctor(dr);
                DateTime dt = data.ExampleTime();

                IEnumerable<Visit> list = testLogic.ShowVisits(dt.Date, dt.Hour.ToString(), 24.ToString(), dr.Surname, "", dr.Specialization, true, 0, 1);

                Assert.AreNotEqual(null, list.First().Date);
            }
        }

        [TestMethod()]
        public void ShowVisitsTestFirstVisit()
        {
            TestDBConnector fakeDBModel = new TestDBConnector();
            Logic testLogic = new Logic(fakeDBModel);
            using (ExampleData data = new ExampleData(0))
            {

                Doctor dr = data.ExampleDoctor();
                Patient p = data.ExamplePatient();
                testLogic.AddDoctor(dr);
                testLogic.CreatePatient(p);
                DateTime dt = data.ExampleTime();
                dt = dt.AddMinutes(60 - dt.Minute);
                for (DateTime _dt = dt; _dt < dt.AddHours(3); _dt = _dt.AddMinutes(15))
                    testLogic.AddVisit(_dt, p, dr);

                IEnumerable<Visit> list = testLogic.ShowVisits(dt.Date, dt.Hour.ToString(), 24.ToString(), dr.Surname, "", dr.Specialization, true, 0, 1);


                Assert.AreEqual(dt.AddHours(3), list.First().DT);
            }
        }

        [TestMethod()]
        public void ShowVisitsTestCount()
        {
            TestDBConnector fakeDBModel = new TestDBConnector();
            Logic testLogic = new Logic(fakeDBModel);
            using (ExampleData data = new ExampleData(0))
            {

                Doctor dr = data.ExampleDoctor();
                Patient p = data.ExamplePatient();
                testLogic.AddDoctor(dr);
                testLogic.CreatePatient(p);
                DateTime dt = data.ExampleTime();
                dt = dt.AddMinutes(60 - dt.Minute);
                for (DateTime _dt = dt; _dt < dt.AddHours(3); _dt = _dt.AddMinutes(15))
                    testLogic.AddVisit(_dt, p, dr);
                dr.LicenseNumber = "55555";
                testLogic.AddDoctor(dr);
                for (DateTime _dt = dt; _dt < dt.AddHours(4); _dt = _dt.AddMinutes(15))
                    testLogic.AddVisit(_dt, p, dr);


                IEnumerable<Visit> list = testLogic.ShowVisits(dt.Date, dt.Hour.ToString(), 24.ToString(), dr.Surname, "", dr.Specialization, true, 0, 50);

                Assert.AreEqual(50, list.Count());
            }
        }

        [TestMethod()]
        public void ShowVisitsTestDoctorsCount()
        {
            TestDBConnector fakeDBModel = new TestDBConnector();
            Logic testLogic = new Logic(fakeDBModel);
            using (ExampleData data = new ExampleData(0))
            {

                Doctor dr = data.ExampleDoctor();
                Patient p = data.ExamplePatient();
                testLogic.AddDoctor(dr);
                testLogic.CreatePatient(p);
                DateTime dt = data.ExampleTime();
                dt = dt.AddMinutes(60 - dt.Minute);
                dr.LicenseNumber = "555555";
                testLogic.AddDoctor(dr);
                dr.LicenseNumber = "123456";
                testLogic.AddDoctor(dr);


                IEnumerable<Visit> list = testLogic.ShowVisits(dt.Date, dt.Hour.ToString(), 24.ToString(), dr.Surname, "", dr.Specialization, true, 0, 50);
                IEnumerable<Visit> drList = list.GroupBy(v => v.Doctor.LicenseNumber).Select(group => group.First());
                Assert.AreEqual(3, drList.Count());
            }
        }


        [TestMethod()]
        public void ShowVisitsTestVisitsCount()
        {
            TestDBConnector fakeDBModel = new TestDBConnector();
            Logic testLogic = new Logic(fakeDBModel);
            using (ExampleData data = new ExampleData(0))
            {

                Doctor dr = data.ExampleDoctor();
                Patient p = data.ExamplePatient();
                testLogic.AddDoctor(dr);
                testLogic.CreatePatient(p);
                DateTime dt = data.ExampleTime();
                dt = dt.AddMinutes(60 - dt.Minute);
                testLogic.AddVisit(dt.AddMinutes(45), p, dr);
                dr.LicenseNumber = "555555";
                testLogic.AddDoctor(dr);
                dr.LicenseNumber = "123456";
                testLogic.AddDoctor(dr);


                IEnumerable<Visit> list = testLogic.ShowVisits(dt.Date, dt.Hour.ToString(), 24.ToString(), dr.Surname, "", dr.Specialization, false, 0, 50);
                IEnumerable<Visit> withPatients = list.Where(v => v.Patient != null);
                Assert.AreEqual(1, withPatients.Count());
            }
        }

        [TestMethod()]
        public void AddDoctorTest()
        {
            TestDBConnector fakeDBModel = new TestDBConnector();
            Logic testLogic = new Logic(fakeDBModel);
            using (ExampleData data = new ExampleData(0))
            {
                Doctor dr1 = data.ExampleDoctor();
                testLogic.AddDoctor(dr1);

                Assert.AreEqual(1, fakeDBModel.__doctors.Count);
            }
        }

        [TestMethod()]
        public void SearchDoctorBySpecSurnameNameTest()
        {
            IDBModels fakeDBModel = new TestDBConnector();
            Logic testLogic = new Logic(fakeDBModel);
            using (ExampleData data = new ExampleData(0))
            {
                Doctor dr1 = data.ExampleDoctor();
                Doctor dr2 = data.ExampleDoctor();
                testLogic.AddDoctor(dr1);
                testLogic.AddDoctor(dr2);

                List<Doctor> list = (List<Doctor>)testLogic.SearchDoctor(dr1.Specialization, dr1.Surname, dr1.Name, "", 0, 1);

                Assert.AreEqual(dr1.LicenseNumber, list[0].LicenseNumber);
            }
        }

        [TestMethod()]
        public void SearchDoctorBySpecTest()
        {
            IDBModels fakeDBModel = new TestDBConnector();
            Logic testLogic = new Logic(fakeDBModel);
            using (ExampleData data = new ExampleData(0))
            {
                Doctor dr1 = data.ExampleDoctor();
                Doctor dr2 = data.ExampleDoctor();
                testLogic.AddDoctor(dr1);
                testLogic.AddDoctor(dr2);

                List<Doctor> list = (List<Doctor>)testLogic.SearchDoctor(dr1.Specialization, "", "", "", 0, 1);

                Assert.AreEqual(dr1.LicenseNumber, list[0].LicenseNumber);
            }
        }

        [TestMethod()]
        public void SearchDoctorBySpecAndSurnameTest()
        {
            IDBModels fakeDBModel = new TestDBConnector();
            Logic testLogic = new Logic(fakeDBModel);
            using (ExampleData data = new ExampleData(0))
            {
                Doctor dr1 = data.ExampleDoctor();
                Doctor dr2 = data.ExampleDoctor();
                testLogic.AddDoctor(dr1);
                testLogic.AddDoctor(dr2);

                List<Doctor> list = (List<Doctor>)testLogic.SearchDoctor(dr1.Specialization, dr1.Surname, "", "", 0, 1);

                Assert.AreEqual(dr1.LicenseNumber, list[0].LicenseNumber);
            }
        }

        [TestMethod()]
        public void DeleteVisitTest1()
        {
            TestDBConnector fakeDBModel = new TestDBConnector();
            Logic testLogic = new Logic(fakeDBModel);
            using (ExampleData data = new ExampleData(0))
            {
                Patient p = data.ExamplePatient();
                Doctor dr = data.ExampleDoctor();
                DateTime dt = data.ExampleTime();
                testLogic.AddDoctor(dr);
                testLogic.CreatePatient(p);
                testLogic.AddVisit(dt, p, dr);

                testLogic.DeleteVisit(dt, dr, p);

                Assert.AreEqual(0, fakeDBModel.__visits.Count);
            }
        }


        [TestMethod()]
        public void DeleteVisitTest2()
        {
            TestDBConnector fakeDBModel = new TestDBConnector();
            Logic testLogic = new Logic(fakeDBModel);
            using (ExampleData data = new ExampleData(0))
            {
                Patient p = data.ExamplePatient();
                Doctor dr = data.ExampleDoctor();
                DateTime dt1 = data.ExampleTime();
                DateTime dt2 = data.ExampleTime();
                testLogic.AddDoctor(dr);
                testLogic.CreatePatient(p);
                testLogic.AddVisit(dt1, p, dr);
                testLogic.AddVisit(dt2, p, dr);

                testLogic.DeleteVisit(dt1, dr, p);

                Assert.AreEqual(1, fakeDBModel.__visits.Count);
            }
        }



    }
}