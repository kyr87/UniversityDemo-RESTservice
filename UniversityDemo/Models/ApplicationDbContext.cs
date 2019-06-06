using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UniversityDemo.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>()
                .HasRequired<User>(m => m.Teacher)
                .WithMany(u => u.TeacherEnrollments)
                .HasForeignKey(m => m.TeacherId);
                //.WillCascadeOnDelete(false);

            modelBuilder.Entity<Enrollment>()
                .HasOptional<User>(m => m.Student)
                .WithMany(u => u.StudentEnrollments)
                .HasForeignKey(m => m.StudentId)
                .WillCascadeOnDelete(false);

        }

    }
}