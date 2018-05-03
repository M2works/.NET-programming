using System;
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
    [Authorize]
    public class VisitTimesController : Controller
    {
        private SurgeryModel db = new SurgeryModel();

        // GET: VisitTimes
        public ActionResult Index()
        {
            var visitTimes = db.VisitTimes.Include(v => v.Doctor).Include(v => v.Patient);
            return View(visitTimes.ToList());
        }

        // GET: VisitTimes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisitTime visitTime = db.VisitTimes.Find(id);
            if (visitTime == null)
            {
                return HttpNotFound();
            }
            return View(visitTime);
        }

        public ActionResult PatientViewDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisitTime visitTime = db.VisitTimes.Find(id);
            if (visitTime == null)
            {
                return HttpNotFound();
            }
            return View(visitTime);
        }

        // GET: VisitTimes/Create
        public ActionResult Create(DateTime Date, int DoctorId)
        {
            var doctor = (from d in db.Doctors
                          where d.Id == DoctorId
                          select d).First();

            VisitTime vt = new VisitTime()
            {
                Date = Date,
                Doctor = doctor
            };
            return View(vt);
        }

        // POST: VisitTimes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Date,DoctorId")] VisitTime visitTime)
        {
            
            int patientId;

            using (Verification v = new Verification(Request))
            {
                patientId = v.GetCurrentUserID();
            }

            var patient = (from p in db.Patients
                            where p.Id == patientId
                            select p).First();

            var doctor = (from d in db.Doctors
                          where d.Id == visitTime.DoctorId
                          select d).First();

            visitTime.Doctor = doctor;
            visitTime.Patient = patient;
            db.VisitTimes.Add(visitTime);
            db.SaveChanges();

            return RedirectToAction("Main", "Patients", new { id = patientId });
            
        }

        // GET: VisitTimes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisitTime visitTime = db.VisitTimes.Find(id);
            if (visitTime == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorId = new SelectList(db.Doctors, "Id", "Name", visitTime.DoctorId);
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "Name", visitTime.PatientId);
            return View(visitTime);
        }

        // POST: VisitTimes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,DoctorId,PatientId")] VisitTime visitTime)
        {
            if (ModelState.IsValid)
            {
                db.Entry(visitTime).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoctorId = new SelectList(db.Doctors, "Id", "Name", visitTime.DoctorId);
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "Name", visitTime.PatientId);
            return View(visitTime);
        }

        // GET: VisitTimes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisitTime visitTime = db.VisitTimes.Find(id);
            if (visitTime == null)
            {
                return HttpNotFound();
            }
            return View(visitTime);
        }

        // POST: VisitTimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VisitTime visitTime = db.VisitTimes.Find(id);
            db.VisitTimes.Remove(visitTime);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PatientViewDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisitTime visitTime = db.VisitTimes.Find(id);
            if (visitTime == null)
            {
                return HttpNotFound();
            }
            return View(visitTime);
        }

        // POST: VisitTimes/Delete/5
        [HttpPost, ActionName("PatientViewDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult PatientViewDeleteConfirmed(int id)
        {
            VisitTime visitTime = db.VisitTimes.Find(id);
            int patientId = visitTime.PatientId;
            db.VisitTimes.Remove(visitTime);
            db.SaveChanges();
            return RedirectToAction("Main", "Patients", new { id = patientId });
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
