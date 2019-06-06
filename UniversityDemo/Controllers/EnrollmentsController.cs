using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityDemo.Models;

namespace UniversityDemo.Controllers
{
    public class EnrollmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Enrollments
        public ActionResult Index(string searching)
        {
            var enrollments = db.Enrollments.Include(e => e.Course).Include(e => e.Student).Include(e => e.Teacher);
            return View(searching == null ? enrollments : enrollments.Where(x => x.Course.CourseName.Contains(searching)));
        }

        // GET: Enrollments/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName");
            ViewBag.StudentId = new SelectList(db.Users.Where(u => u.UserRole == Role.Student), "UserId", "UserName");
            ViewBag.TeacherId = new SelectList(db.Users.Where(u => u.UserRole == Role.Teacher), "UserId", "UserName");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnrollmentId,TeacherId,StudentId,CourseId")] Enrollment enrollment)
        {
            var check = db.Enrollments.Any(c => c.TeacherId == enrollment.TeacherId && c.StudentId == enrollment.StudentId && c.CourseId == enrollment.CourseId);
            if (ModelState.IsValid)
            {
                if (!check)
                {
                    db.Enrollments.Add(enrollment);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                TempData["Message"] = "This enrollment already exists. Try another!";
                return RedirectToAction("Create");

            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", enrollment.CourseId);
            ViewBag.StudentId = new SelectList(db.Users.Where(u => u.UserRole == Role.Student), "UserId", "UserName", enrollment.StudentId);
            ViewBag.TeacherId = new SelectList(db.Users.Where(u => u.UserRole == Role.Teacher), "UserId", "UserName", enrollment.TeacherId);
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", enrollment.CourseId);
            ViewBag.StudentId = new SelectList(db.Users.Where(u => u.UserRole == Role.Student), "UserId", "UserName", enrollment.StudentId);
            ViewBag.TeacherId = new SelectList(db.Users.Where(u => u.UserRole == Role.Teacher), "UserId", "UserName", enrollment.TeacherId);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnrollmentId,TeacherId,StudentId,CourseId")] Enrollment enrollment)
        {
            var check = db.Enrollments.Any(c => c.TeacherId == enrollment.TeacherId && c.StudentId == enrollment.StudentId && c.CourseId == enrollment.CourseId);
            if (ModelState.IsValid)
            {
                if (!check)
                {
                    db.Entry(enrollment).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                TempData["Message"] = "This enrollment already exists. Try another!";
                return RedirectToAction("Edit");
                
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", enrollment.CourseId);
            ViewBag.StudentId = new SelectList(db.Users.Where(u => u.UserRole == Role.Student), "UserId", "UserName", enrollment.StudentId);
            ViewBag.TeacherId = new SelectList(db.Users.Where(u => u.UserRole == Role.Teacher), "UserId", "UserName", enrollment.TeacherId);
            return View(enrollment);
        }

        public ActionResult Delete(int id)
        {
            Enrollment enrollment = db.Enrollments.Find(id);
            db.Enrollments.Remove(enrollment);
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
