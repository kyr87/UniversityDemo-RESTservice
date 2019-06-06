using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityDemo.Dtos
{
    public class EnrollmentDto
    {
        public int EnrollmentId { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}