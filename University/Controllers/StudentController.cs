using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using University.Models;

namespace University.Controllers
{
    [Authorize(Roles = "admin")]
    public class StudentController : Controller
    {
        private UsersContext db = new UsersContext();
        private int _pageSize = 10;

        private void SetViewBag(Group group = null)
        {
            ViewBag.Groups = group == null
                ? new SelectList(db.Groups, "GroupId", "Specialization")
                : new SelectList(db.Groups, "GroupId", "Specialization", group.GroupId);            
        }

        //
        // GET: /Student/

        public ActionResult Index(int? page)
        {
            return View(db.Students
                .Include(s => s.Group)
                .Include(s => s.UserProfile)
                .OrderBy(s => s.RecordBookNumber).ToPagedList((page ?? 1), _pageSize));
        }

        //
        // GET: /Student/Details/5

        public ActionResult Details(int id = 0)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        //
        // GET: /Student/Create

        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        //
        // POST: /Student/Create

        [HttpPost]
        public ActionResult Create(Student student)
        {
            var studentDup = db.Students.FirstOrDefault(
                s => (s.AdrS == student.AdrS) && (s.DataS == student.DataS) && (s.GroupId == student.GroupId) &&
                     (s.Name == student.Name) && (s.RecordBookNumber == student.RecordBookNumber) &&
                     (s.Rik == student.Rik));
            if (studentDup != null)
            {
                ModelState.AddModelError("", "Вже існує");
            }
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                Student.AddStudentToUserDatabase(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            SetViewBag(student.Group);
            return View(student);
        }

        //
        // GET: /Student/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            SetViewBag(student.Group);
            return View(student);
        }

        //
        // POST: /Student/Edit/5

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            SetViewBag(student.Group);
            return View(student);
        }

        //
        // GET: /Student/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        //
        // POST: /Student/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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