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
    public class UsersController : Controller
    {
        private IGenericRepository<User> data;

        public UsersController()
        {
            this.data = new Repository<User>();
        }

        public UsersController(IGenericRepository<User> data)
        {
            this.data = data;
        }

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Users
        public ActionResult Index(string searching)
        {
            return View(searching == null ? data.GetAll() : data.GetAll().Where(x => x.UserName.Contains(searching)).ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = data.GetById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,UserName,Email,UserRole")] User user)
        {
            if (ModelState.IsValid)
            {
                bool checkMail = data.EmailExists(user.Email);

                if (checkMail)
                {
                    ModelState.AddModelError(string.Empty, "Email already exists.");
                    return View(user);
                }
                data.Insert(user);
                data.Save();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = data.GetById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,UserName,Email,UserRole")] User user)
        {
            if (ModelState.IsValid)
            {
                data.Update(user);
                data.Save();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult Delete(int id)
        {
            var enrollments = db.Enrollments.Where(en => en.StudentId == id).ToList();
            foreach (Enrollment enrol in enrollments)
            {
                db.Enrollments.Remove(enrol);
            }
            db.SaveChanges();

            data.Delete(id);
            data.Save();
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
