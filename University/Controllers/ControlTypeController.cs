using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using University.Models;

namespace University.Controllers
{
    [Authorize(Roles = "admin, teacher")]
    public class ControlTypeController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /ControlType/

        public ActionResult Index()
        {
            return View(db.ControlTypes.ToList());
        }

        //
        // GET: /ControlType/Details/5

        public ActionResult Details(int id = 0)
        {
            ControlType controltype = db.ControlTypes.Find(id);
            if (controltype == null)
            {
                return HttpNotFound();
            }
            return View(controltype);
        }

        //
        // GET: /ControlType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ControlType/Create

        [HttpPost]
        public ActionResult Create(ControlType controltype)
        {
            var controlTypeDup = db.ControlTypes.FirstOrDefault(c => c.Name == controltype.Name.Trim());
            if (controlTypeDup != null)
            {
                ModelState.AddModelError("Name", "Вже існує");
            }
            if (string.IsNullOrWhiteSpace(controltype.Name))
            {
                ModelState.AddModelError("Name", "Необхідно ввести назву");
            }
            if (ModelState.IsValid)
            {
                db.ControlTypes.Add(controltype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(controltype);
        }

        //
        // GET: /ControlType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ControlType controltype = db.ControlTypes.Find(id);
            if (controltype == null)
            {
                return HttpNotFound();
            }
            return View(controltype);
        }

        //
        // POST: /ControlType/Edit/5

        [HttpPost]
        public ActionResult Edit(ControlType controltype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(controltype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(controltype);
        }

        //
        // GET: /ControlType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ControlType controltype = db.ControlTypes.Find(id);
            if (controltype == null)
            {
                return HttpNotFound();
            }
            return View(controltype);
        }

        //
        // POST: /ControlType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ControlType controltype = db.ControlTypes.Find(id);
            db.ControlTypes.Remove(controltype);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}