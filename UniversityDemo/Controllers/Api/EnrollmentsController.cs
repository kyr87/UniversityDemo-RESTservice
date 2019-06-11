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
    public class EnrollmentsController : ApiController
    {
        private ApplicationDbContext _context;

        public EnrollmentsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/enrollments
        public IHttpActionResult GetEnrollments()
        {
            var enrollmentDtos = _context.Enrollments
                .ToList()
                .Select(Mapper.Map<Enrollment, EnrollmentDto>);

            return Ok(enrollmentDtos);
        }

        // GET /api/enrollments/1
        public IHttpActionResult GetEnrollment(int id)
        {
            var enrollment = _context.Enrollments.SingleOrDefault(c => c.EnrollmentId == id);

            if (enrollment == null)
                return NotFound();

            return Ok(Mapper.Map<Enrollment, EnrollmentDto>(enrollment));
        }

        // POST /api/enrollments
        [HttpPost]
        public IHttpActionResult CreateEnrollment(EnrollmentDto enrollmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model is not valid. Check again!!!");

            var enrollment = Mapper.Map<EnrollmentDto, Enrollment>(enrollmentDto);
            _context.Enrollments.Add(enrollment);
            _context.SaveChanges();

            enrollmentDto.EnrollmentId = enrollment.EnrollmentId;
            return Created(new Uri(Request.RequestUri + "/" + enrollment.EnrollmentId), enrollmentDto);
        }

        // PUT /api/enrollments/1
        [HttpPut]
        public IHttpActionResult UpdateEnrollment(int id, EnrollmentDto enrollmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model is not valid. Check again!!!");

            var enrollmentInDb = _context.Enrollments.SingleOrDefault(c => c.EnrollmentId == id);

            if (enrollmentInDb == null)
                return NotFound();

            Mapper.Map(enrollmentDto, enrollmentInDb);

            _context.SaveChanges();

            return Ok("Enrollment updated successfully");
        }

        // DELETE /api/enrollments/1
        [HttpDelete]
        public IHttpActionResult DeleteEnrollment(int id)
        {
            var enrollmentInDb = _context.Enrollments.SingleOrDefault(c => c.EnrollmentId == id);

            if (enrollmentInDb == null)
                return NotFound();

            _context.Enrollments.Remove(enrollmentInDb);
            _context.SaveChanges();

            return Ok("Enrollment deleted successfully");
        }
    }
}
