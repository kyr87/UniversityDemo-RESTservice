using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityDemo.Dtos
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        [Required]
        public string CourseName { get; set; }
    }
}