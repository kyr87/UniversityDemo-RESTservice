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
    public class UsersController : ApiController
    {
        private ApplicationDbContext _context;

        public UsersController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/users
        public IHttpActionResult GetUsers()
        {
            var userDtos = _context.Users
                .ToList()
                .Select(Mapper.Map<User, UserDto>);

            return Ok(userDtos);
        }

        // GET /api/users/1
        public IHttpActionResult GetUser(int id)
        {
            var user = _context.Users.SingleOrDefault(c => c.UserId == id);

            if (user == null)
                return NotFound();

            return Ok(Mapper.Map<User, UserDto>(user));
        }

        // POST /api/users
        [HttpPost]
        public IHttpActionResult CreateUser(UserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = Mapper.Map<UserDto, User>(userDto);
            _context.Users.Add(user);
            _context.SaveChanges();

            userDto.UserId = user.UserId;
            return Created(new Uri(Request.RequestUri + "/" + user.UserId), userDto);
        }

        // PUT /api/users/1
        [HttpPut]
        public IHttpActionResult UpdateUser(int id, UserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var userInDb = _context.Users.SingleOrDefault(c => c.UserId == id);

            if (userInDb == null)
                return NotFound();

            Mapper.Map(userDto, userInDb);

            _context.SaveChanges();

            return Ok();
        }

        // DELETE /api/users/1
        [HttpDelete]
        public IHttpActionResult DeleteUser(int id)
        {
            var userInDb = _context.Users.SingleOrDefault(c => c.UserId == id);

            var enrollments = _context.Enrollments.Where(en => en.StudentId == id).ToList();
            foreach (Enrollment enrol in enrollments)
            {
                _context.Enrollments.Remove(enrol);
            }
            _context.SaveChanges();

            if (userInDb == null)
                return NotFound();

            _context.Users.Remove(userInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
