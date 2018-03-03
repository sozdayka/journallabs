using JournalLabs.API.BLL;
using JournalLabs.API.Models;
using JournalLabs.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JournalLabs.API.Controllers
{
    [RoutePrefix("api/StudentGroup")]
    public class StudentGroupController : ApiController
    {
        public StudentGroupService _studentGroupService;
        public StudentGroupController()
        {
            _studentGroupService = new StudentGroupService();
        }
        //[Route("GetStudentGroups")]
        //[HttpGet]
        //public IHttpActionResult GetStudentGroups()
        //{
        //    var studentGroups = _studentGroupService.GetStudentGroups();
        //    return Ok(studentGroups);
        //}
        [Route("CreateStudentGroup")]
        [HttpPost]
        public IHttpActionResult CreateStudentGroup(StudentGroup studentGroup)
        {
            _studentGroupService.CreateStudentGroup(studentGroup);
            return Ok("Good");
        }
        [Route("AddStudentToGroup")]
        [HttpPost]
        public IHttpActionResult AddStudentToGroup(AddStudentToGroupViewModel addStudentToGroupViewModel)
        {
            var response = _studentGroupService.AddStudentToGroup(addStudentToGroupViewModel);
            return Ok(response);
        }
        [Route("UpdateStudentGroup")]
        [HttpPost]
        public IHttpActionResult UpdateStudentGroup(StudentGroup studentGroup)
        {
            _studentGroupService.UpdateStudentGroup(studentGroup);
            return Ok("Good");
        }
        [Route("GetStudentGroupById")]
        [HttpGet]
        public IHttpActionResult GetStudentGroupById(string Id)
        {
            var result = _studentGroupService.GetStudentGroupById(Id);
            return Ok(result);
        }
        [Route("DeleteStudentGroupById")]
        [HttpGet]
        public IHttpActionResult DeleteStudentGroupById(string Id)
        {
            _studentGroupService.DeleteStudentGroupById(Id);
            return Ok("Good");
        }
    }
}
