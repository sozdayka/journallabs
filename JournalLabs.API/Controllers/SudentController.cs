using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JournalLabs.API.BLL;
using JournalLabs.API.Models;
using JournalLabs.API.BLL;
using JournalLabs.API.Models;

namespace StudentLabs.API.Controllers
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
        public IHttpActionResult GetStudents()
        {
            return Ok("Good");
        }
        [Route("CreateStudent")]
        public IHttpActionResult CreateStudent(Student student)
        {
            _studentService.CreateStudent(student);
            return Ok("Good");
        }
        [Route("UpdateStudent")]
        public IHttpActionResult UpdateStudent(Student student)
        {
            _studentService.UpdateStudent(student);
            return Ok("Good");
        }
        [Route("GetStudentById")]
        public IHttpActionResult GetStudentById(string Id)
        {
            var result = _studentService.GetStudentById(Id);
            return Ok(result);
        }
        [Route("DeleteStudentById")]
        public IHttpActionResult DeleteStudentById(string Id)
        {
            _studentService.DeleteStudentById(Id);
            return Ok("Good");
        }
    }
}
