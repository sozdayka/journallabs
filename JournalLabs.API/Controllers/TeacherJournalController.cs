using JournalLabs.API.BLL;
using JournalLabs.API.Models;
using JournalLabs.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JournalLabs.API.Controllers
{
    [RoutePrefix("api/TeacherJournal")]
    public class TeacherJournalController : ApiController
    {
        public TeacherJournalService _userService;
        public TeacherJournalController()
        {
            _userService = new TeacherJournalService();
        }
        [Route("AddTeacherToJournal")]
        [HttpPost]
        public IHttpActionResult AddTeacherToJournal(TeacherJournal teacher)
        {
            _userService.AddTeacherToJournal(teacher);
            return Ok("Good");
        }
        [Route("DeleteTeacherFromJournal")]
        [HttpGet]
        public IHttpActionResult DeleteTeacherFromJournal(string Id)
        {
            _userService.DeleteTeacherFromJournal(Id);
            return Ok("Good");
        }
        [Route("GetAllJournalAssistants")]
        [HttpGet]
        public IHttpActionResult GetAllJournalAssistants(string journalId)
        {
            var result = _userService.GetAllJournalAssistants(journalId);
            return Ok(result);
        }
    }
}
