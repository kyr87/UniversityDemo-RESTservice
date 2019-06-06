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
    public class CoursesController : Controller
    {
        private IGenericRepository<Course> data;

        public CoursesController()
        {
            this.data = new Repository<Course>();
        }

        public CoursesController(IGenericRepository<Course> data)
        {
            this.data = data;
        }

        // GET: Courses
        public ActionResult Index(string searching)
        {
            return View(searching == null ? data.GetAll() : data.GetAll().Where(x => x.CourseName.Contains(searching)).ToList());
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId,CourseName")] Course course)
        {
            
            if (ModelState.IsValid)
            {
                bool checkName = data.CourseExists(course.CourseName);

                if (checkName)
                {
                    ModelState.AddModelError(string.Empty, "Course already exists.");
                    return View(course);
                }
                data.Insert(course);
                data.Save();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = data.GetById(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseId,CourseName")] Course course)
        {
            if (ModelState.IsValid)
            {
                bool checkName = data.CourseExists(course.CourseName);

                if (checkName)
                {
                    ModelState.AddModelError(string.Empty, "Course already exists.");
                    return View(course);
                }
                data.Update(course);
                data.Save();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        public ActionResult Delete(int id)
        {          
            data.Delete(id);
            data.Save();
            return RedirectToAction("Index");
        }
    }
}
