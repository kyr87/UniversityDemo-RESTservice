using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityDemo.Models
{
    public class Course
    {
        public Course()
        {
            this.Enrollment = new HashSet<Enrollment>();
        }

        public int CourseId { get; set; }
        [Required]
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }

        public ICollection<Enrollment> Enrollment { get; set; }

    }
}