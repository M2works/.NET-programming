using Microsoft.VisualStudio.TestTools.UnitTesting;
using Doktor_i_Pacjent_2017_XD_Pro.DBConnectionLogic;
using Doktor_i_Pacjent_2017_XD_Pro.DBConnectionLogic.SurgeryModel;
using System;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Doktor_i_Pacjent_2017_XD_Pro.DBConnectionLogic.Tests
{
    [TestClass()]
    public class DBModelsTests
    {

        #region unitTests

        [TestMethod()]
        public void AddPatientTest()
        {
            var mockSetPatients = new Mock<DbSet<Patient>>();

            var mockContext = new Mock<SurgeryModelContainer>();
            mockContext.Setup(m => m.Patients).Returns(mockSetPatients.Object);

            var service = new DBModels(mockContext.Object);
            service.AddPatient("Kamil", "Opalach", "111111111", "22222222222", new Address());

            mockSetPatients.Verify(m => m.Add(It.IsAny<Patient>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
                

        [TestMethod()]
        public void ListPatientsByNameAndSurnameTest()
        {
            var patients = new List<Patient>()
            {
                new Patient() { Id=1, PhoneNumber = "12456343", Name = "John", Surname = "Smith", Address = new Address(), PESELNumber = "11111111111" },
                new Patient() { Id=2, PhoneNumber = "87984564", Name = "Pete", Surname = "Luck", Address = new Address(), PESELNumber = "22222222222" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Patient>>();
            mockSet.As<IQueryable<Patient>>().Setup(m => m.Provider).Returns(patients.Provider);
            mockSet.As<IQueryable<Patient>>().Setup(m => m.Expression).Returns(patients.Expression);
            mockSet.As<IQueryable<Patient>>().Setup(m => m.ElementType).Returns(patients.ElementType);
            mockSet.As<IQueryable<Patient>>().Setup(m => m.GetEnumerator()).Returns(patients.GetEnumerator());

            var mockContext = new Mock<SurgeryModelContainer>();
            mockContext.Setup(c => c.Patients).Returns(mockSet.Object);

            var service = new DBModels(mockContext.Object);
            var patientsAll = service.ListPatientsByNameAndSurname("John", "Smith", 0, 10);

            Assert.AreEqual(1, patientsAll.Count);
            Assert.AreEqual("John", patientsAll[0].Name);
        }

        [TestMethod()]
        public void ListPatientsBySurnameAndCityTest()
        {
            var patients = new List<Patient>()
            {
                new Patient() { Id=1, PhoneNumber = "12456343", Name = "John", Surname = "Smith", Address = new Address {City = "Warsaw" }, PESELNumber = "11111111111" },
                new Patient() { Id=2, PhoneNumber = "87984564", Name = "Pete", Surname = "Luck", Address = new Address {City = "Poznań" }, PESELNumber = "22222222222" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Patient>>();
            mockSet.As<IQueryable<Patient>>().Setup(m => m.Provider).Returns(patients.Provider);
            mockSet.As<IQueryable<Patient>>().Setup(m => m.Expression).Returns(patients.Expression);
            mockSet.As<IQueryable<Patient>>().Setup(m => m.ElementType).Returns(patients.ElementType);
            mockSet.As<IQueryable<Patient>>().Setup(m => m.GetEnumerator()).Returns(patients.GetEnumerator());

            var mockContext = new Mock<SurgeryModelContainer>();
            mockContext.Setup(c => c.Patients).Returns(mockSet.Object);

            var service = new DBModels(mockContext.Object);
            var patientsAll = service.ListPatientsByNameSurnameAndCity("John", "Smith", "Warsaw", 0, 10);

            Assert.AreEqual(1, patientsAll.Count);
            Assert.AreEqual("John", patientsAll[0].Name);
        }

        [TestMethod()]
        public void ListPatientsByPESELTest()
        {
            var patients = new List<Patient>()
            {
                new Patient() { Id=1, PhoneNumber = "12456343", Name = "John", Surname = "Smith", Address = new Address {City = "Warsaw" }, PESELNumber = "11111111111" },
                new Patient() { Id=2, PhoneNumber = "87984564", Name = "Pete", Surname = "Luck", Address = new Address {City = "Poznań" }, PESELNumber = "22222222222" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Patient>>();
            mockSet.As<IQueryable<Patient>>().Setup(m => m.Provider).Returns(patients.Provider);
            mockSet.As<IQueryable<Patient>>().Setup(m => m.Expression).Returns(patients.Expression);
            mockSet.As<IQueryable<Patient>>().Setup(m => m.ElementType).Returns(patients.ElementType);
            mockSet.As<IQueryable<Patient>>().Setup(m => m.GetEnumerator()).Returns(patients.GetEnumerator());

            var mockContext = new Mock<SurgeryModelContainer>();
            mockContext.Setup(c => c.Patients).Returns(mockSet.Object);

            var service = new DBModels(mockContext.Object);
            var patientsAll = service.ListPatientsByPESEL("11111111111", 0, 10);

            Assert.AreEqual(1, patientsAll.Count);
            Assert.AreEqual("John", patientsAll[0].Name);
        }

        [TestMethod()]
        public void ListPatientsBySurnameTest()
        {
            var patients = new List<Patient>()
            {
                new Patient() { Id=1, PhoneNumber = "12456343", Name = "John", Surname = "Smith", Address = new Address {City = "Warsaw" }, PESELNumber = "11111111111" },
                new Patient() { Id=2, PhoneNumber = "87984564", Name = "Pete", Surname = "Luck", Address = new Address {City = "Poznań" }, PESELNumber = "22222222222" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Patient>>();
            mockSet.As<IQueryable<Patient>>().Setup(m => m.Provider).Returns(patients.Provider);
            mockSet.As<IQueryable<Patient>>().Setup(m => m.Expression).Returns(patients.Expression);
            mockSet.As<IQueryable<Patient>>().Setup(m => m.ElementType).Returns(patients.ElementType);
            mockSet.As<IQueryable<Patient>>().Setup(m => m.GetEnumerator()).Returns(patients.GetEnumerator());

            var mockContext = new Mock<SurgeryModelContainer>();
            mockContext.Setup(c => c.Patients).Returns(mockSet.Object);

            var service = new DBModels(mockContext.Object);

            var patientsAll = service.ListPatientsBySurname("Smith", 0, 10);

            Assert.AreEqual(1, patientsAll.Count);
            Assert.AreEqual("Smith", patientsAll[0].Surname);
        }

        [TestMethod()]
        public void ListPatientsByNameSurnameAndCityTest()
        {
            var patients = new List<Patient>()
            {
                new Patient() { Id=1, PhoneNumber = "12456343", Name = "John", Surname = "Smith", Address = new Address {City = "Warsaw" }, PESELNumber = "11111111111" },
                new Patient() { Id=2, PhoneNumber = "87984564", Name = "Pete", Surname = "Luck", Address = new Address {City = "Poznań" }, PESELNumber = "22222222222" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Patient>>();
            mockSet.As<IQueryable<Patient>>().Setup(m => m.Provider).Returns(patients.Provider);
            mockSet.As<IQueryable<Patient>>().Setup(m => m.Expression).Returns(patients.Expression);
            mockSet.As<IQueryable<Patient>>().Setup(m => m.ElementType).Returns(patients.ElementType);
            mockSet.As<IQueryable<Patient>>().Setup(m => m.GetEnumerator()).Returns(patients.GetEnumerator());

            var mockContext = new Mock<SurgeryModelContainer>();
            mockContext.Setup(c => c.Patients).Returns(mockSet.Object);

            var service = new DBModels(mockContext.Object);
            var patientsAll = service.ListPatientsByNameSurnameAndCity("John", "Smith", "Warsaw", 0, 10);

            Assert.AreEqual(1, patientsAll.Count);
            Assert.AreEqual("John", patientsAll[0].Name);
            Assert.AreEqual("Smith", patientsAll[0].Surname);
            Assert.AreEqual("Warsaw", patientsAll[0].Address.City);
        }        

        #endregion

        #region integrationTests

        [TestMethod()]
        public void AddEditandDeletePatientTest()
        {
            Patient patient = new Patient
            {
                Name = "ExampleName",
                Surname = "ExampleSurname",
                PESELNumber = "00000000000",
                PhoneNumber = "000000000",
                Address = new Address
                {
                    City = "ExampleCity",
                    Country = "ExampleCountry",
                    HomeNumber = 0,
                    Street = "ExampleStreet",
                    StreetNumber = 0
                }
            };

            SurgeryModelContainer smc = new SurgeryModelContainer();
            IDBModels dbm = new DBModels(smc);

            int beforeAddPatientsCount = smc.Patients.Count();
            dbm.AddPatient(patient.Name, patient.Surname, patient.PhoneNumber, patient.PESELNumber, patient.Address);
            int afterAddPatientsCount = smc.Patients.Count();

            dbm.EditPatient(patient.PESELNumber, patient.Name, "EditedSurname", patient.PhoneNumber, patient.PESELNumber, patient.Address);
            var editedPatient = dbm.ListPatientsByPESEL("00000000000", 0, 10);
            int numberOfEditedPatients = editedPatient.Count;

            dbm.DeletePatient("00000000000");
            int afterDeletePatientsNumber = smc.Patients.Count();

            Assert.AreEqual(afterAddPatientsCount, beforeAddPatientsCount + 1);
            Assert.AreEqual(afterAddPatientsCount, afterDeletePatientsNumber + 1);
            Assert.AreEqual("EditedSurname", editedPatient[0].Surname);
        }

        [TestMethod()]
        public void AccessDatabaseTest()
        {
            SurgeryModelContainer smc = new SurgeryModelContainer();
            IDBModels dbm = new DBModels(smc);

            Assert.AreNotEqual(smc.Database, null);
        }

        [TestMethod()]
        public void DatabaseNameConnectedTest() // znak - do spytania
        {
            SurgeryModelContainer smc = new SurgeryModelContainer();
            IDBModels dbm = new DBModels(smc);

            Assert.AreEqual(smc.Database.Connection.Database, "ClinicDB");
        }

        [TestMethod()]
        public void ConnectionStringNameTest() // znak - do spytania
        {
            SurgeryModelContainer smc = new SurgeryModelContainer();
            IDBModels dbm = new DBModels(smc);
            
            Assert.AreEqual(smc.Database.Connection.DataSource, "NOTEBOOK96\\SQLEXPRESS");
        }

        [TestMethod()]
        public void ClosedConnectionTest()
        {
            SurgeryModelContainer smc = new SurgeryModelContainer();
            IDBModels dbm = new DBModels(smc);

            System.Data.Common.DbConnection dbcon = smc.Database.Connection;

            Assert.AreEqual(smc.Database.Connection.State, ConnectionState.Closed);
        }
        
        [TestMethod()]
        public void OpenConnectionTest()
        {
            SurgeryModelContainer smc = new SurgeryModelContainer();
            IDBModels dbm = new DBModels(smc);

            System.Data.Common.DbConnection dbcon = smc.Database.Connection;
            dbcon.Open();

            Assert.AreEqual(smc.Database.Connection.State, ConnectionState.Open);
        }

        #endregion
    }
}