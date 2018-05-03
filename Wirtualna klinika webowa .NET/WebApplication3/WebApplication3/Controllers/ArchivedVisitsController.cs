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

namespace WebApplication3.Controllers
{
    public class ArchivedVisitsController : Controller
    {
        private SurgeryModel db = new SurgeryModel();

        // GET: ArchivedVisits
        public ActionResult Index()
        {
            var archivedVisits = db.ArchivedVisits.Include(a => a.Doctor).Include(a => a.Patient);
            return View(archivedVisits.ToList());
        }

        // GET: ArchivedVisits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArchivedVisit archivedVisit = db.ArchivedVisits.Find(id);
            if (archivedVisit == null)
            {
                return HttpNotFound();
            }
            return View(archivedVisit);
        }

        // GET: ArchivedVisits/Create
        public ActionResult Create()
        {
            ViewBag.DoctorId = new SelectList(db.Doctors, "Id", "Name");
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "Name");
            return View();
        }

        // POST: ArchivedVisits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,DoctorId,PatientId,Rating")] ArchivedVisit archivedVisit)
        {
            if (ModelState.IsValid)
            {
                db.ArchivedVisits.Add(archivedVisit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DoctorId = new SelectList(db.Doctors, "Id", "Name", archivedVisit.DoctorId);
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "Name", archivedVisit.PatientId);
            return View(archivedVisit);
        }

        // GET: ArchivedVisits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArchivedVisit archivedVisit = db.ArchivedVisits.Find(id);
            if (archivedVisit == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorId = new SelectList(db.Doctors, "Id", "Name", archivedVisit.DoctorId);
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "Name", archivedVisit.PatientId);
            return View(archivedVisit);
        }

        // POST: ArchivedVisits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,DoctorId,PatientId,Rating")] ArchivedVisit archivedVisit)
        {

            var visit = (from av in db.ArchivedVisits
                         where av.Id == archivedVisit.Id
                         select av).FirstOrDefault();

            int? oldrating = visit.Rating;
            visit.Rating = archivedVisit.Rating.Value;
           
            int countErrors = 0;

            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    countErrors++;
                }
            }

            if (countErrors == 2)
            {
                db.Entry(visit).State = EntityState.Modified;
                db.Entry(visit.Patient).State = EntityState.Unchanged;
                db.Entry(visit.Doctor).State = EntityState.Unchanged;

                db.SaveChanges();

                db.Entry(visit.Patient).State = EntityState.Unchanged;
                db.Entry(visit).State = EntityState.Unchanged;

                SetRating(visit.DoctorId, visit.Doctor.RatingsCount, oldrating, archivedVisit.Rating.Value);

                return RedirectToAction("Main", "Patients", new { id = visit.PatientId });
            }

            ViewBag.DoctorId = new SelectList(db.Doctors, "Id", "Name", archivedVisit.DoctorId);
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "Name", archivedVisit.PatientId);
            return View(archivedVisit);
        }

        private void SetRating(int doctorId, int ratingsCount, int? oldRating, int newRating)
        {
            double sum = 0;

            var doctor = (from d in db.Doctors
                            where d.Id == doctorId
                            select d).First();

            if (doctor.AverageRating != null && doctor.RatingsCount != 0)
                sum = doctor.AverageRating.Value * doctor.RatingsCount;

            if (oldRating == null)
            {
                sum += newRating;
                doctor.RatingsCount += 1;
            }
            else
            {
                sum -= oldRating.Value;
                sum += newRating;
            }
            if (doctor.RatingsCount > 0)
                doctor.AverageRating = sum / doctor.RatingsCount;
            else
                doctor.AverageRating = 0;

            db.Entry(doctor).State = EntityState.Modified;
            db.SaveChanges();
        }

        // GET: ArchivedVisits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArchivedVisit archivedVisit = db.ArchivedVisits.Find(id);
            if (archivedVisit == null)
            {
                return HttpNotFound();
            }
            return View(archivedVisit);
        }

        // POST: ArchivedVisits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArchivedVisit archivedVisit = db.ArchivedVisits.Find(id);
            db.ArchivedVisits.Remove(archivedVisit);
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
