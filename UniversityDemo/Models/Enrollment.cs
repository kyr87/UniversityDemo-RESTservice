using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityDemo.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int TeacherId { get; set; }
        public int? StudentId { get; set; }
        public int CourseId { get; set; }

        public Course Course { get; set; }
        public User Teacher { get; set; }
        public User Student { get; set; }
    }
}