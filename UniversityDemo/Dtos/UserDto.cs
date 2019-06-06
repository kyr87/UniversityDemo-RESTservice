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
        public string UserName { get; set; }
        public string Email { get; set; }
        public Role UserRole { get; set; }
    }
}