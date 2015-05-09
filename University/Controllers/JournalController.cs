using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Generator;
using System.Web.Routing;
using PagedList;
using University.Models;

namespace University.Controllers
{
    [Authorize]
    public class JournalController : Controller
    {
        private UsersContext db = new UsersContext();
        private int _pageSize = 10;

        private void SetViewBag(Student student = null, Subject subject = null, 
            ControlType controlType = null, int? groupId = null, string mark = null)
        {
            ViewBag.Students = student == null
                ? new SelectList(db.Students.OrderBy(s => s.Name), "StudentId", "Name")
                : new SelectList(db.Students.OrderBy(s => s.Name), "StudentId", "Name", student.StudentId);
            SetSubjectsToBag(subject);
            ViewBag.ControlTypes = controlType == null
                ? new SelectList(db.ControlTypes, "ControlTypeId", "Name")
                : new SelectList(db.ControlTypes, "ControlTypeId", "Name", controlType.ControlTypeId);
            ViewBag.Groups = groupId == null
                ? new SelectList(db.Groups, "GroupId", "Specialization")
                : new SelectList(db.Groups, "GroupId", "Specialization", groupId);
            ViewBag.Marks = mark == null
                ? new SelectList(Enumerable.Range(1, 100))
                : new SelectList(Enumerable.Range(1, 100), mark);
        }

        private void SetSubjectsToBag(Subject subject)
        {
            var subjects = db.Subjects.ToList();
            if (User.IsInRole("teacher"))
            {
                var teacher = GetCurrentTeacher();
                subjects = subjects.Where(s => teacher != null && s.TeacherId == teacher.TeacherId).ToList();
            }
            ViewBag.Subjects = subject == null
                ? new SelectList(subjects, "SubjectId", "Name")
                : new SelectList(subjects, "SubjectId", "Name", subject.SubjectId);
        }

        private Teacher GetCurrentTeacher()
        {
            var teacher = db.Teachers.Include(t => t.UserProfile)
                .FirstOrDefault(t => t.UserProfile.UserName == User.Identity.Name);
            return teacher;
        }

        //
        // GET: /Journal/

        public ActionResult Index(int? page, int? groupId, int? subjectId, int? controlTypeId,
            int? currentGroupId, int? currentSubjectId, int? currentControlTypeId, string studentName,
            string currentStudentName)
        {
            var journal = db.Journals
                .Include(j => j.Student)
                .Include(j => j.Student.UserProfile)
                .Include(j => j.Subject)
                .Include(j => j.ControlType)
                .Include(j => j.Student.Group);
            if ((groupId != null) || (subjectId != null) || (controlTypeId != null) || (studentName != null))
            {
                page = 1;
            }
            groupId = groupId ?? currentGroupId;
            subjectId = subjectId ?? currentSubjectId;
            controlTypeId = controlTypeId ?? currentControlTypeId;
            studentName = studentName ?? currentStudentName;
            if (User.IsInRole("student"))
            {
                var student = db.Students.FirstOrDefault(s => s.UserProfile.UserName == User.Identity.Name);
                studentName = student != null ? student.Name : studentName;
            }
            ViewBag.CurrentGroupId = groupId;
            ViewBag.CurrentSubjectId = subjectId;
            ViewBag.CurrentControlTypeId = controlTypeId;
            ViewBag.CurrentStudentName = studentName;
            if (groupId != null)
            {
                journal = journal.Where(j => j.Student.Group.GroupId == groupId);
            }
            if (subjectId != null)
            {
                journal = journal.Where(j => j.Subject.SubjectId == subjectId);
            }
            if (controlTypeId != null)
            {
                journal = journal.Where(j => j.ControlType.ControlTypeId == controlTypeId);
            }
            if (!string.IsNullOrWhiteSpace(studentName))
            {
                journal = journal.Where(j => j.Student.Name.ToLower().Contains(studentName.ToLower()));
            }
            SetViewBag(null,
                db.Subjects.FirstOrDefault(s => s.SubjectId == currentSubjectId),
                db.ControlTypes.FirstOrDefault(c => c.ControlTypeId == currentControlTypeId),
                currentGroupId);
            if (User.IsInRole("teacher"))
            {
                var teacher = GetCurrentTeacher();
                journal = journal.Where(j => j.Subject.TeacherId == teacher.TeacherId);
            }
            return View(journal.OrderBy(j => j.Student.RecordBookNumber)
                .ThenBy(j => j.Subject.Name)
                .ThenBy(j => j.ControlType.Name)
                .ToPagedList((page ?? 1), _pageSize));
        }

        //
        // GET: /Journal/Details/5

        public ActionResult Details(int id = 0, int? page = null)
        {
            Journal journal = db.Journals.Include(j => j.Student.Group).FirstOrDefault(j => j.JournalId == id);
            if (journal == null)
            {
                return HttpNotFound();
            }
            ViewBag.PageNum = page;
            return View(journal);
        }

        //
        // GET: /Journal/Create
        [Authorize(Roles = "admin, teacher")]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        //
        // POST: /Journal/Create

        [Authorize(Roles = "admin, teacher")]
        [HttpPost]
        public ActionResult Create(Journal journal, int? page = null)
        {
            if (ModelState.IsValid)
            {
                db.Journals.Add(journal);
                db.SaveChanges();
                return RedirectToAction("Index", new {page});
            }
            SetViewBag(journal.Student, journal.Subject, journal.ControlType);
            return View(journal);
        }

        //
        // GET: /Journal/Edit/5
        [Authorize(Roles = "admin, teacher")]
        public ActionResult Edit(int id = 0)
        {
            Journal journal = db.Journals.Find(id);
            if (journal == null)
            {
                return HttpNotFound();
            }
            SetViewBag(journal.Student, journal.Subject, journal.ControlType, mark: journal.Mark);
            return View(journal);
        }

        //
        // POST: /Journal/Edit/5
        [Authorize(Roles = "admin, teacher")]
        [HttpPost]
        public ActionResult Edit(Journal journal, int? page = null)
        {
            if (ModelState.IsValid)
            {
                db.Entry(journal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new {page});
            }
            SetViewBag(journal.Student, journal.Subject, journal.ControlType, mark : journal.Mark);
            return View(journal);
        }

        //
        // GET: /Journal/Delete/5
        [Authorize(Roles = "admin, teacher")]
        public ActionResult Delete(int id = 0)
        {
            Journal journal = db.Journals.Find(id);
            if (journal == null)
            {
                return HttpNotFound();
            }
            return View(journal);
        }

        //
        // POST: /Journal/Delete/5
        [Authorize(Roles = "admin, teacher")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id, int? page = null)
        {
            Journal journal = db.Journals.Find(id);
            db.Journals.Remove(journal);
            db.SaveChanges();
            return RedirectToAction("Index", new {page});
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}