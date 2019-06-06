using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityDemo.Dtos;
using UniversityDemo.Models;

namespace UniversityDemo.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<User, UserDto>();
            Mapper.CreateMap<UserDto, User>();

            Mapper.CreateMap<Course, CourseDto>();
            Mapper.CreateMap<CourseDto, Course>();

            Mapper.CreateMap<Enrollment, EnrollmentDto>();
            Mapper.CreateMap<EnrollmentDto, Enrollment>();
        }
    }
}