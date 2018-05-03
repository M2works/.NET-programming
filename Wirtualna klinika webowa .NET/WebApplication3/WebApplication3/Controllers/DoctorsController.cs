using System;
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
    using Models;
    [Authorize]
    public class DoctorsController : Controller
    {
        private SurgeryModel db = new SurgeryModel();

        // GET: Doctors
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
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
            var doctors = from p in db.Doctors
                           select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                doctors = from pa in db.Doctors
                           where pa.Name.Contains(searchString)
                                       || pa.Surname.Contains(searchString)
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
                case "Name":
                    doctors = doctors.OrderBy(s => s.Name);
                    break;
                case "name_desc":
                    doctors = doctors.OrderByDescending(s => s.Name);
                    break;
                default:
                    doctors = doctors.OrderBy(s => s.Surname);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(doctors.ToPagedList(pageNumber, pageSize));
        }

        public List<VisitTime> GenerateVisits(Doctor d)
        {
            int days = 10;    // tu dac dni
            DateTime dt = DateTime.Today;
            DateTime dt1 = DateTime.Today;
            dt1 = dt1.AddHours(DateTime.Now.Hour);
            dt1 = dt1.AddMinutes(DateTime.Now.Minute < 15 ? 15 : DateTime.Now.Minute < 30 ? 30 : DateTime.Now.Minute < 45 ? 45 : 60);
            var returnList = new List<VisitTime>();

            Patient p;
            using (Verification v = new Verification(Request))
            {
                int PatientId = v.GetCurrentUserID();
                p = (from pat in db.Patients
                     where pat.Id == PatientId
                     select pat).First();
            }
                for (int i = 0; i < days; i++)
                {
                    dt = dt.AddHours(d.StartingHour);
                    dt = dt1 > dt ? dt1 : dt;
                    dt1 = DateTime.Today;

                    while (dt.Hour < d.EndingHour)
                    {
                        returnList.Add(new VisitTime()
                        {
                            Doctor = d,
                            Date = dt
                        });

                        dt = dt.AddMinutes(15);

                    }
                    dt = dt.Date.AddDays(1);
                    if (dt.DayOfWeek == DayOfWeek.Saturday) dt = dt.AddDays(2);
                }

            var list = ListVisitsByLicenceNumber(d.LicenseNumber);

            foreach (var v in list)
            {
                returnList.ForEach(visit => { if (visit.Date == v.Date) visit.Patient = v.Patient; });
            }

            returnList.RemoveAll(visit => visit.Patient != null);

            return returnList;
        }

        public List<VisitTime> ListVisitsByLicenceNumber(string licenceNumber)
        {
            List<VisitTime> visits;

            using (SurgeryModel sc = new SurgeryModel())
            {
                visits = (from visit in sc.VisitTimes
                          where visit.Doctor.LicenseNumber == licenceNumber
                          orderby visit.Date
                          select visit).ToList();

                foreach (VisitTime vt in visits)
                {
                    sc.Entry(vt).Reference(x => x.Doctor).Load();
                    sc.Entry(vt.Doctor).Reference(x => x.Clinic).Load();
                    sc.Entry(vt).Reference(x => x.Patient).Load();
                }
            }

            return visits;
        }

        // GET: Doctors
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
            var doctors = from p in db.Doctors
                          select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                doctors = from pa in db.Doctors
                          where pa.Name.Contains(searchString)
                                      || pa.Surname.Contains(searchString)
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
                case "Name":
                    doctors = doctors.OrderBy(s => s.Name);
                    break;
                case "name_desc":
                    doctors = doctors.OrderByDescending(s => s.Name);
                    break;
                default:
                    doctors = doctors.OrderBy(s => s.Surname);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(doctors.ToPagedList(pageNumber, pageSize));
        }

        // GET: Doctors/Details/5
        public ActionResult Details(int? id)
        {
            using (Verification v = new Verification(Request)) if (!v.IsAdmin()) return RedirectToAction("InsufficientPermission", "Account");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        public ActionResult DisplayList(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            List<VisitTime> doctorVisits = GenerateVisits(doctor);

            DoctorVisitsManager dvm = new DoctorVisitsManager(doctor, doctorVisits);
            return View(dvm);
        }

        // GET: Doctors/Create
        public ActionResult Create()
        {
            using (Verification v = new Verification(Request)) if (!v.IsAdmin()) return RedirectToAction("InsufficientPermission", "Account");
            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "Name");
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Surname,ClinicId,StartingHour,EndingHour,Specialization,VisitPrice,LicenseNumber,DoctorAccount_Password")] Doctor doctor)
        {
            using (Verification v = new Verification(Request)) if (!v.IsAdmin()) return RedirectToAction("InsufficientPermission", "Account");
            doctor.RatingsCount = 0;
            doctor.AverageRating = 0;
            doctor.DoctorAccount_Login = doctor.LicenseNumber;

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
                var doctors = (from d in db.Doctors
                               where d.LicenseNumber == doctor.LicenseNumber
                               select d).ToList();

                if(doctor.StartingHour > doctor.EndingHour)
                {
                    ModelState.AddModelError("TimeError", "Godzina rozpoczęcia przyjęć musi być wcześniejsza od godziny zakończenia");
                    ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "Name", doctor.ClinicId);
                    return View(doctor);
                }

                if (doctors.Count == 1)
                {
                    ModelState.AddModelError("Error", "Lekarz o takim numerze licencji już istnieje");
                    ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "Name", doctor.ClinicId);
                    return View(doctor);
                }

                db.Doctors.Add(doctor);
                db.SaveChanges();
                return RedirectToAction("AdminIndex");
            }

            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "Name", doctor.ClinicId);
            return View(doctor);
        }

        // GET: Doctors/Edit/5
        public ActionResult Edit(int? id)
        {
            using (Verification v = new Verification(Request)) if (!v.IsAdmin()) return RedirectToAction("InsufficientPermission", "Account");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "Name", doctor.ClinicId);
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Surname,ClinicId,StartingHour,EndingHour,AverageRating,RatingsCount,Specialization,VisitPrice,LicenseNumber,DoctorAccount_Login,DoctorAccount_Password")] Doctor doctor)
        {
            using (Verification v = new Verification(Request)) if (!v.IsAdmin()) return RedirectToAction("InsufficientPermission", "Account");
            if (ModelState.IsValid)
            {
                var doctors = (from d in db.Doctors
                               where d.LicenseNumber == doctor.LicenseNumber && d.Id != doctor.Id
                               select d).ToList();

                if (doctor.StartingHour > doctor.EndingHour)
                {
                    ModelState.AddModelError("TimeError", "Godzina rozpoczęcia przyjęć musi być wcześniejsza od godziny zakończenia");
                    ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "Name", doctor.ClinicId);
                    return View(doctor);
                }

                if (doctors.Count == 1)
                {
                    ModelState.AddModelError("Error", "Lekarz o takim numerze licencji już istnieje");
                    ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "Name", doctor.ClinicId);
                    return View(doctor);
                }

                db.Entry(doctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AdminIndex");
            }
            ViewBag.ClinicId = new SelectList(db.Clinics, "Id", "Name", doctor.ClinicId);
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public ActionResult Delete(int? id)
        {
            using (Verification v = new Verification(Request)) if (!v.IsAdmin()) return RedirectToAction("InsufficientPermission", "Account");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (Verification v = new Verification(Request)) if (!v.IsAdmin()) return RedirectToAction("InsufficientPermission", "Account");
            Doctor doctor = db.Doctors.Find(id);
            db.Doctors.Remove(doctor);
            db.SaveChanges();
            return RedirectToAction("AdminIndex");
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
