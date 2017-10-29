using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JournalLabs.API.BLL;
using JournalLabs.API.Models;

namespace JournalLabs.API.Controllers
{
    [RoutePrefix("api/Student")]
    public class StudentController : ApiController
    {
        public StudentService _studentService;
        public StudentController()
        {
            _studentService = new StudentService();
        }
        [Route("GetStudents")]
        public IHttpActionResult GetJournals()
        {
            return Ok("Good");
        }
        [Route("CreateStudent")]
        public IHttpActionResult CreateStudent(Student student)
        {
            return Ok("Good");
        }
        [Route("UpdateStudent")]
        public IHttpActionResult UpdateStudent(Student student)
        {
            return Ok("Good");
        }
        [Route("GetStudentById")]
        public IHttpActionResult GetStudentById(string Id)
        {
            return Ok("Good");
        }
        [Route("DeleteStudentById")]
        public IHttpActionResult DeleteStudentById(string Id)
        {
            return Ok("Good");
        }
    }
}
