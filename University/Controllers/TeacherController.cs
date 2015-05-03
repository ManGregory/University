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
    public class TeacherController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Teacher/

        public ActionResult Index()
        {
            return View(db.Teachers.Include(t => t.UserProfile).ToList());
        }

        //
        // GET: /Teacher/Details/5

        public ActionResult Details(int id = 0)
        {
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        //
        // GET: /Teacher/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Teacher/Create
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(Teacher teacher)
        {
            var teacherDup = db.Teachers.FirstOrDefault(
                t => (t.AdrV == teacher.AdrV) && (t.DataV == teacher.DataV) && (t.Name == teacher.Name.Trim()) &&
                     (t.Zvaniya == teacher.Zvaniya.Trim()));
            if (teacherDup != null)
            {
                ModelState.AddModelError("", "Вже існує");
            }
            if (string.IsNullOrWhiteSpace(teacher.Name))
            {
                ModelState.AddModelError("Name", "Необхідно ввести ПІБ викладача");
            }
            if (ModelState.IsValid)
            {
                db.Teachers.Add(teacher);
                db.SaveChanges();
                Teacher.AddTeacherToUserDatabase(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(teacher);
        }

        //
        // GET: /Teacher/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id = 0)
        {
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        //
        // POST: /Teacher/Edit/5
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(Teacher teacher)
        {
            if (string.IsNullOrWhiteSpace(teacher.Name))
            {
                ModelState.AddModelError("Name", "Необхідно ввести ПІБ викладача");
            }
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teacher);
        }

        //
        // GET: /Teacher/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id = 0)
        {
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        //
        // POST: /Teacher/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
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