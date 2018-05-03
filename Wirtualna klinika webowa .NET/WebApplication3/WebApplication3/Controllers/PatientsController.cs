using System;
using System.Globalization;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3;
using PagedList;

namespace WebApplication3.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private SurgeryModel db = new SurgeryModel();

        // GET: Patients
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            using (Verification v = new Verification(Request)) if (!v.IsAdmin()) return RedirectToAction("InsufficientPermission", "Account");
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SurnameSortParm = String.IsNullOrEmpty(sortOrder) ? "surname_desc" : "";
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var patients = from p in db.Patients
                           select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                patients = from pa in db.Patients
                           where pa.Name.Contains(searchString)
                                       || pa.Surname.Contains(searchString)
                           select pa;
            }
            else
            {
                patients = from pa in db.Patients
                           select pa;
            }

            switch (sortOrder)
            {
                case "surname_desc":
                    patients = patients.OrderByDescending(s => s.Surname);
                    break;
                case "Name":
                    patients = patients.OrderBy(s => s.Name);
                    break;
                case "name_desc":
                    patients = patients.OrderByDescending(s => s.Name);
                    break;
                default:
                    patients = patients.OrderBy(s => s.Surname);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(patients.ToPagedList(pageNumber, pageSize));
        }
        // GET: Patients
        public ActionResult AdminIndex(string sortOrder, string currentFilter, string searchString, int? page)
        {
            using (Verification v = new Verification(Request)) if (!v.IsAdmin()) return RedirectToAction("InsufficientPermission", "Account");
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SurnameSortParm = String.IsNullOrEmpty(sortOrder) ? "surname_desc" : "";
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var patients = from p in db.Patients
                           select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                patients = from pa in db.Patients
                           where pa.Name.Contains(searchString)
                                       || pa.Surname.Contains(searchString)
                           select pa;
            }
            else
            {
                patients = from pa in db.Patients
                           select pa;
            }

            switch (sortOrder)
            {
                case "surname_desc":
                    patients = patients.OrderByDescending(s => s.Surname);
                    break;
                case "Name":
                    patients = patients.OrderBy(s => s.Name);
                    break;
                case "name_desc":
                    patients = patients.OrderByDescending(s => s.Name);
                    break;
                default:
                    patients = patients.OrderBy(s => s.Surname);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(patients.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult History(int? id)
        {
            using (Verification v = new Verification(Request)) if (!v.IsAdmin() && v.GetCurrentUserID() != id) id = v.GetCurrentUserID();
            if (id == null || id<0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }

            var archivedVisits = (from visits in db.ArchivedVisits
                                  where visits.PatientId == patient.Id
                                  select visits).ToList();

            ViewBag.patientId = id;
            return View(archivedVisits);
        }
        // GET: Patients/Details/5
        public ActionResult Details(int? id)
        {
            using (Verification v = new Verification(Request)) if (!v.IsAdmin()) return RedirectToAction("InsufficientPermission", "Account");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }
        // GET: Patients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Surname,PhoneNumber,PESELNumber,PatientAddress_City,PatientAddress_Street,PatientAddress_StreetNumber,PatientAddress_HomeNumber,PatientAddress_PostalCode,PatientAccount_Password")] Patient model)
        {
            using (Verification v = new Verification(Request)) if (!v.IsAdmin()) return RedirectToAction("InsufficientPermission", "Account");
            ModelState.SetModelValue("PatientAccount_Login", new ValueProviderResult(model.PESELNumber, null, CultureInfo.InvariantCulture));
            model.PatientAccount_Login = model.PESELNumber;

            ModelState.SetModelValue("PatientAddress_Country", new ValueProviderResult("Polska", null, CultureInfo.InvariantCulture));
            model.PatientAddress_Country = "Polska";

            int countErrors = 0;

            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    countErrors++;
                }
            }

            if (countErrors == 1)
            {
                var patient = new Patient
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    PESELNumber = model.PESELNumber,
                    PhoneNumber = model.PhoneNumber,
                    PatientAddress_Country = model.PatientAddress_Country,
                    PatientAddress_City = model.PatientAddress_City,
                    PatientAddress_Street = model.PatientAddress_Street,
                    PatientAddress_PostalCode = model.PatientAddress_PostalCode,
                    PatientAddress_StreetNumber = model.PatientAddress_StreetNumber,
                    PatientAccount_Login = model.PESELNumber,
                    PatientAccount_Password = model.PatientAccount_Password,
                    PatientAddress_HomeNumber = model.PatientAddress_HomeNumber == null ? String.Empty : model.PatientAddress_HomeNumber
                };

                using (SurgeryModel db = new SurgeryModel())
                {
                    var patients = (from p in db.Patients
                                    where p.PatientAccount_Login == model.PESELNumber
                                    select p).ToList();

                    if (patients.Count == 1)
                    {
                        ModelState.AddModelError("Error", "Pacjent o takim numerze PESEL już istnieje");
                        return View(model);
                    }
                }

                using (SurgeryModel db = new SurgeryModel())
                {
                    db.Patients.Add(patient);
                    db.SaveChanges();
                }

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(int? id)
        {
            using (Verification v = new Verification(Request)) if (!v.IsAdmin()) return RedirectToAction("InsufficientPermission", "Account");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        public ActionResult PatientEdit(int? id)
        {
            using (Verification v = new Verification(Request)) if (!v.IsAdmin() && v.GetCurrentUserID() != id) id = v.GetCurrentUserID();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        public ActionResult Main(int? id)
        {
            using (Verification v = new Verification(Request)) if (!v.IsAdmin() && v.GetCurrentUserID() != id) id = v.GetCurrentUserID();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }

            CheckPatientVisitsForArchived(patient.PatientAccount_Login);
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Surname,PhoneNumber,PESELNumber,PatientAddress_Country,PatientAddress_City,PatientAddress_Street,PatientAddress_StreetNumber,PatientAddress_HomeNumber,PatientAddress_PostalCode,PatientAccount_Login,PatientAccount_Password")] Patient model)
        {
            using (Verification v = new Verification(Request)) if (!v.IsAdmin()) return RedirectToAction("InsufficientPermission", "Account");
            ModelState.SetModelValue("PatientAccount_Login", new ValueProviderResult(model.PESELNumber, null, CultureInfo.InvariantCulture));
            model.PatientAccount_Login = model.PESELNumber;

            ModelState.SetModelValue("PatientAddress_Country", new ValueProviderResult("Polska", null, CultureInfo.InvariantCulture));
            model.PatientAddress_Country = "Polska";

            int countErrors = 0;

            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    countErrors++;
                }
            }

            if (countErrors <= 2)
            {
                var patient = (from p in db.Patients
                               where p.Id == model.Id
                               select p).FirstOrDefault();

                var oldPESEL = patient.PESELNumber;

                patient.Name = model.Name;
                patient.Surname = model.Surname;
                patient.PESELNumber = model.PESELNumber;
                patient.PhoneNumber = model.PhoneNumber;
                patient.PatientAddress_Country = model.PatientAddress_Country;
                patient.PatientAddress_City = model.PatientAddress_City;
                patient.PatientAddress_Street = model.PatientAddress_Street;
                patient.PatientAddress_PostalCode = model.PatientAddress_PostalCode;
                patient.PatientAddress_StreetNumber = model.PatientAddress_StreetNumber;
                patient.PatientAccount_Login = model.PESELNumber;
                patient.PatientAccount_Password = model.PatientAccount_Password;
                patient.PatientAddress_HomeNumber = model.PatientAddress_HomeNumber == null ? String.Empty : model.PatientAddress_HomeNumber;

                if (oldPESEL != model.PESELNumber)
                {
                    using (SurgeryModel db = new SurgeryModel())
                    {
                        var patients = (from p in db.Patients
                                        where p.PatientAccount_Login == model.PESELNumber
                                        select p).ToList();

                        if (patients.Count == 1)
                        {
                            ModelState.AddModelError("Error", "Pacjent o takim numerze PESEL już istnieje");
                            return View(model);
                        }
                    }
                }

                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("AdminIndex", "Patients");
            }
            return View(model);
        }

        public virtual void CheckPatientVisitsForArchived(string login)
        {
            using (SurgeryModel sm = new SurgeryModel())
            {
                var patient = (from pt in sm.Patients
                               where pt.PatientAccount_Login == login
                               select pt).First();

                DateTime dtp = DateTime.Now.AddDays(14);

                var visits = (from vt in sm.VisitTimes
                              where vt.PatientId == patient.Id && vt.Date < dtp
                              select vt).ToList();

                DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                foreach (VisitTime visit in visits)
                {
                    if (visit.Date <= DateTime.Now)
                    {
                        ArchivedVisit av = new ArchivedVisit()
                        {
                            Patient = visit.Patient,
                            Doctor = visit.Doctor,
                            Date = visit.Date
                        };
                        sm.ArchivedVisits.Add(av);
                        sm.VisitTimes.Remove(visit);
                    }
                }

                sm.SaveChanges();
            }
        }

        public ActionResult Search(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SurnameSortParm = String.IsNullOrEmpty(sortOrder) ? "surname_desc" : "";
            ViewBag.ClinicSortParm = sortOrder == "Clinic" ? "clinic_desc" : "Clinic";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var doctors = from p in db.Doctors
                          select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                doctors = from pa in db.Doctors
                          where pa.Name.Contains(searchString)
                                      || pa.Clinic.Name.Contains(searchString) || pa.Surname.Contains(searchString)
                          select pa;
            }
            else
            {
                doctors = from pa in db.Doctors
                          select pa;
            }

            switch (sortOrder)
            {
                case "surname_desc":
                    doctors = doctors.OrderByDescending(s => s.Surname);
                    break;
                case "Clinic":
                    doctors = doctors.OrderBy(s => s.Clinic.Name);
                    break;
                case "clinic_desc":
                    doctors = doctors.OrderByDescending(s => s.Clinic.Name);
                    break;
                default:
                    doctors = doctors.OrderBy(s => s.Surname);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(doctors.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PatientEdit([Bind(Include = "Id,Name,Surname,PhoneNumber,PESELNumber,PatientAddress_Country,PatientAddress_City,PatientAddress_Street,PatientAddress_StreetNumber,PatientAddress_HomeNumber,PatientAddress_PostalCode,PatientAccount_Login,PatientAccount_Password")] Patient model)
        {
            //using (Verification v = new Verification(Request)) if (!v.IsAdmin() && v.GetCurrentUserID() != id) id = v.GetCurrentUserID();
            ModelState.SetModelValue("PatientAccount_Login", new ValueProviderResult(model.PESELNumber, null, CultureInfo.InvariantCulture));
            model.PatientAccount_Login = model.PESELNumber;

            ModelState.SetModelValue("PatientAddress_Country", new ValueProviderResult("Polska", null, CultureInfo.InvariantCulture));
            model.PatientAddress_Country = "Polska";

            int countErrors = 0;

            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    countErrors++;
                }
            }

            if (countErrors <= 2)
            {
                var patient = (from p in db.Patients
                               where p.Id == model.Id
                               select p).FirstOrDefault();

                var oldPESEL = patient.PESELNumber;

                patient.Name = model.Name;
                patient.Surname = model.Surname;
                patient.PESELNumber = model.PESELNumber;
                patient.PhoneNumber = model.PhoneNumber;
                patient.PatientAddress_Country = model.PatientAddress_Country;
                patient.PatientAddress_City = model.PatientAddress_City;
                patient.PatientAddress_Street = model.PatientAddress_Street;
                patient.PatientAddress_PostalCode = model.PatientAddress_PostalCode;
                patient.PatientAddress_StreetNumber = model.PatientAddress_StreetNumber;
                patient.PatientAccount_Login = model.PESELNumber;
                patient.PatientAccount_Password = model.PatientAccount_Password;
                patient.PatientAddress_HomeNumber = model.PatientAddress_HomeNumber == null ? String.Empty : model.PatientAddress_HomeNumber;

                if (oldPESEL != model.PESELNumber)
                {
                    using (SurgeryModel db = new SurgeryModel())
                    {
                        var patients = (from p in db.Patients
                                        where p.PatientAccount_Login == model.PESELNumber
                                        select p).ToList();

                        if (patients.Count == 1)
                        {
                            ModelState.AddModelError("Error", "Pacjent o takim numerze PESEL już istnieje");
                            return View(model);
                        }
                    }
                }

                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                using (Verification v = new Verification(Request))
                    if (v.IsAdmin()) return RedirectToAction("AdminIndex", "Patients");
                    else return RedirectToAction("Main", "Patients", new { id = patient.Id });
            }
            return View(model);
        }

        // GET: Patients/Delete/5
        public ActionResult Delete(int? id)
        {
            using (Verification v = new Verification(Request)) if (!v.IsAdmin()) return RedirectToAction("InsufficientPermission", "Account");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (Verification v = new Verification(Request)) if (!v.IsAdmin()) return RedirectToAction("InsufficientPermission", "Account");
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
