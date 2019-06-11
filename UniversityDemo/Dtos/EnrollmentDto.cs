using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityDemo.Dtos
{
    public class EnrollmentDto
    {
        public int EnrollmentId { get; set; }
        [Required]
        public int TeacherId { get; set; }
        public int? StudentId { get; set; }
        [Required]
        public int CourseId { get; set; }
    }
}