using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using University.Models;

namespace University.Controllers
{
    [Authorize(Roles = "admin, teacher, student")]
    public class SubjectController : Controller
    {
        private UsersContext db = new UsersContext();

        private void SetViewBag(Teacher teacher = null)
        {
            ViewBag.Teachers = teacher == null
                ? new SelectList(db.Teachers, "TeacherId", "Name")
                : new SelectList(db.Teachers, "TeacherId", "Name", teacher.TeacherId);
        }

        //
        // GET: /Subject/

        public ActionResult Index()
        {
            return View(db.Subjects.Include(s => s.Teacher).ToList());
        }

        //
        // GET: /Subject/Details/5

        public ActionResult Details(int id = 0)
        {
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        //
        // GET: /Subject/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        //
        // POST: /Subject/Create

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(Subject subject)
        {
            var subjectDup = db.Subjects.FirstOrDefault(s => (s.Name == subject.Name) && (s.TeacherId == subject.TeacherId));
            if (subjectDup != null)
            {
                ModelState.AddModelError("Name", "Вже існує");
            }

            if (ModelState.IsValid)
            {
                db.Subjects.Add(subject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            SetViewBag(subject.Teacher);
            return View(subject);
        }

        //
        // GET: /Subject/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id = 0)
        {
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            SetViewBag(subject.Teacher);
            return View(subject);
        }

        //
        // POST: /Subject/Edit/5

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            SetViewBag(subject.Teacher);
            return View(subject);
        }

        //
        // GET: /Subject/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id = 0)
        {
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        //
        // POST: /Subject/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Subject subject = db.Subjects.Find(id);
            db.Subjects.Remove(subject);
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