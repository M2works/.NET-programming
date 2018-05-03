using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            using (Verification v = new Verification(Request)) if (!v.IsAdmin()) return RedirectToAction("InsufficientPermission", "Account");

           return View();
        }
    }
}