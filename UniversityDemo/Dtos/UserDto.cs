using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UniversityDemo.Models;

namespace UniversityDemo.Dtos
{
    public class UserDto
    {
        public int UserId { get; set; }  
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public Role UserRole { get; set; }
    }
}