using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityDemo.Models
{
    public class Repository<T> : IGenericRepository<T> where T : class
    { 
        private  ApplicationDbContext _db;

        public Repository()
        {
            this._db = new ApplicationDbContext();
        }

        public Repository(ApplicationDbContext _db)
        {
            this._db = _db;
        }
              
        public IEnumerable<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }

        public T GetById(object id)
        {
            return _db.Set<T>().Find(id);
        }

        public void Insert(T model)
        {
             _db.Set<T>().Add(model);                    
        }
      
        public void Update(T model)
        {
            _db.Entry(model).State = EntityState.Modified; 
        }

        public void Delete(object id)
        {
            T existing = GetById(id);
            _db.Set<T>().Remove(existing);
        }

        public bool EmailExists(string mail)
        {
            return _db.Users.Any(u => u.Email == mail);           
        }

        public bool CourseExists(string name)
        {
            return _db.Courses.Any(u => u.CourseName == name);
        }

        public void Save()
        {
            using (_db)
            {
                _db.SaveChanges();
            }
        }
    }
}