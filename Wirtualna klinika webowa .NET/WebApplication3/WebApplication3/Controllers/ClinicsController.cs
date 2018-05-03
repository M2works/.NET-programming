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
    public class ClinicsController : Controller
    {
        private SurgeryModel db = new SurgeryModel();

        // GET: Clinics
        public ActionResult Index()
        {
            return View(db.Clinics.ToList());
        }

        // GET: Clinics
        public ActionResult AdminIndex()
        {
            using (Verification v = new Verification(Request)) if (!v.IsAdmin()) return RedirectToAction("InsufficientPermission", "Account");
            return View(db.Clinics.ToList());
        }


        // GET: Clinics/Details/5
        public ActionResult Details(int? id)
        {
            using (Verification v = new Verification(Request)) if (!v.IsAdmin()) return RedirectToAction("InsufficientPermission", "Account");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinic clinic = db.Clinics.Find(id);
            if (clinic == null)
            {
                return HttpNotFound();
            }
            return View(clinic);
        }

        // GET: Clinics/Create
        public ActionResult Create()
        {
            using (Verification v = new Verification(Request)) if (!v.IsAdmin()) return RedirectToAction("InsufficientPermission", "Account");
            return View();
        }

        // POST: Clinics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ClinicAddress_Country,ClinicAddress_City,ClinicAddress_Street,ClinicAddress_StreetNumber,ClinicAddress_HomeNumber,ClinicAddress_PostalCode")] Clinic clinic)
        {
            using (Verification v = new Verification(Request)) if (!v.IsAdmin()) return RedirectToAction("InsufficientPermission", "Account");
            if (clinic.ClinicAddress_HomeNumber == null) clinic.ClinicAddress_HomeNumber = string.Empty;
            if (ModelState.IsValid)
            {
                db.Clinics.Add(clinic);
                db.SaveChanges();
                return RedirectToAction("AdminIndex");
            }

            return View(clinic);
        }

        // GET: Clinics/Edit/5
        public ActionResult Edit(int? id)
        {
            using (Verification v = new Verification(Request)) if (!v.IsAdmin()) return RedirectToAction("InsufficientPermission", "Account");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinic clinic = db.Clinics.Find(id);
            if (clinic == null)
            {
                return HttpNotFound();
            }
            return View(clinic);
        }

        // POST: Clinics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ClinicAddress_Country,ClinicAddress_City,ClinicAddress_Street,ClinicAddress_StreetNumber,ClinicAddress_HomeNumber,ClinicAddress_PostalCode")] Clinic clinic)
        {
            using (Verification v = new Verification(Request)) if (!v.IsAdmin()) return RedirectToAction("InsufficientPermission", "Account");
            if (clinic.ClinicAddress_HomeNumber == null) clinic.ClinicAddress_HomeNumber = string.Empty;
            if (ModelState.IsValid)
            {
                db.Entry(clinic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AdminIndex");
            }
            return View(clinic);
        }

        // GET: Clinics/Delete/5
        public ActionResult Delete(int? id)
        {
            using (Verification v = new Verification(Request)) if (!v.IsAdmin()) return RedirectToAction("InsufficientPermission", "Account");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinic clinic = db.Clinics.Find(id);
            if (clinic == null)
            {
                return HttpNotFound();
            }
            return View(clinic);
        }

        // POST: Clinics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (Verification v = new Verification(Request)) if (!v.IsAdmin()) return RedirectToAction("InsufficientPermission", "Account");
            Clinic clinic = db.Clinics.Find(id);
            db.Clinics.Remove(clinic);
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
