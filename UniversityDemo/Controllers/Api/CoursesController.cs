using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UniversityDemo.Dtos;
using UniversityDemo.Models;

namespace UniversityDemo.Controllers.Api
{
    public class CoursesController : ApiController
    {
        private ApplicationDbContext _context;

        public CoursesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/courses
        public IHttpActionResult GetCourses()
        {
            var courseDtos = _context.Courses
                .ToList()
                .Select(Mapper.Map<Course, CourseDto>);

            return Ok(courseDtos);
        }

        // GET /api/courses/1
        public IHttpActionResult GetCourse(int id)
        {
            var course = _context.Courses.SingleOrDefault(c => c.CourseId == id);

            if (course == null)
                return NotFound();

            return Ok(Mapper.Map<Course, CourseDto>(course));
        }

        // POST /api/courses
        [HttpPost]
        public IHttpActionResult CreateCourse(CourseDto courseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model is not valid. Check again!!!");

            var course = Mapper.Map<CourseDto, Course>(courseDto);
            _context.Courses.Add(course);
            _context.SaveChanges();

            courseDto.CourseId = course.CourseId;
            return Created(new Uri(Request.RequestUri + "/" + course.CourseId), courseDto);
        }

        // PUT /api/courses/1
        [HttpPut]
        public IHttpActionResult UpdateCourse(int id, CourseDto courseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model is not valid. Check again!!!");

            var courseInDb = _context.Courses.SingleOrDefault(c => c.CourseId == id);

            if (courseInDb == null)
                return NotFound();

            Mapper.Map(courseDto, courseInDb);

            _context.SaveChanges();

            return Ok($"{courseInDb.CourseName} updated successfully");
        }

        // DELETE /api/courses/1
        [HttpDelete]
        public IHttpActionResult DeleteCourse(int id)
        {
            var courseInDb = _context.Courses.SingleOrDefault(c => c.CourseId == id);

            if (courseInDb == null)
                return NotFound();

            _context.Courses.Remove(courseInDb);
            _context.SaveChanges();

            return Ok($"{courseInDb.CourseName} deleted successfully");
        }
    }
}
