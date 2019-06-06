using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityDemo.Models
{
    public class User
    {
        public User()
        {
            this.TeacherEnrollments = new HashSet<Enrollment>();
            this.StudentEnrollments = new HashSet<Enrollment>();
        }

        public int UserId { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Role")]
        [EnumDataType(typeof(Role))]
        public Role UserRole { get; set; }

        public ICollection<Enrollment> TeacherEnrollments { get; set; }
        public ICollection<Enrollment> StudentEnrollments { get; set; }
    }

    public enum Role
    {
        Teacher,
        Student
    }
}