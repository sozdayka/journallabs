using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using JournalLabs.API.BLL;
using JournalLabs.API.Models;
using JournalLabs.API.BLL;
using JournalLabs.API.Models;

namespace StudentLabs.API.Controllers
{
    [EnableCors(origins: "http://localhost:62106", headers: "*", methods: "*")]
    //[EnableCors(origins: "http://localhost:54500", headers: "*", methods: "*")]
    [RoutePrefix("api/Student")]
    public class StudentController : ApiController
    {
        public StudentService _studentService;
        public LabBlockService _labBlockService;
        public StudentController()
        {
            _studentService = new StudentService();
            _labBlockService = new LabBlockService();
        }
        [Route("GetStudents")]
        [HttpGet]
        public IHttpActionResult GetStudents()
        {
            return Ok("Good");
        }
        [Route("CreateStudent")]
        [HttpPost]
        public IHttpActionResult CreateStudent(Student student)
        {
            _studentService.CreateStudent(student);
            return Ok("Good");
        }
        [Route("UpdateStudent")]
        [HttpPost]
        public IHttpActionResult UpdateStudent(Student student)
        {
            _studentService.UpdateStudent(student);
            return Ok("Good");
        }
        [Route("GetStudentById")]
        [HttpGet]
        public IHttpActionResult GetStudentById(string Id)
        {
            var result = _studentService.GetStudentById(Id);
            return Ok(result);
        }
        [Route("DeleteStudentById")]
        [HttpGet]
        public IHttpActionResult DeleteStudentById(string Id)
        {
            _studentService.DeleteStudentById(Id);
            _labBlockService.DeleteLabBlockByStudentId(Id);
            return Ok("Good");
        }
    }
}
