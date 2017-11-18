using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using JournalLabs.API.BLL;
using JournalLabs.API.Models;

namespace JournalLabs.API.Controllers
{
    [EnableCors(origins: "http://localhost:62106", headers: "*", methods: "*")]
    [RoutePrefix("api/Journal")]
    public class JournalController : ApiController
    {
        public JournalService _journalService;
        public JournalController()
        {
            _journalService = new JournalService();
        }
        [Route("GetJournals")]
        [HttpGet]
        public IHttpActionResult GetJournals()
        {           
            return Ok("Good");
        }
        [Route("CreateJournal")]
        [HttpGet]
        public IHttpActionResult CreateJournal(string lessonName ,int studentsCount,int labBlocksCount, Guid teacherId)
        {
             _journalService.CreateJournal(lessonName, studentsCount, labBlocksCount,teacherId);
            return Ok("Good");
        }
        [Route("UpdateJournal")]
        [HttpPost]
        public IHttpActionResult UpdateJournal(Journal journal)
        {
            _journalService.UpdateJournal(journal);
            return Ok("Good");
        }
        [Route("GetJournalById")]
        [HttpGet]
        public IHttpActionResult GetJournalById(string Id)
        {
            var result = _journalService.GetJournalById(Id);
            return Ok(result);
        }
        [Route("DeleteJournalById")]
        [HttpGet]
        public IHttpActionResult DeleteJournalById(string Id)
        {
            _journalService.DeleteJournalById(Id);
            return Ok("Good");
        }
        [Route("GetAllJournalsByTeacherId")]
        [HttpGet]
        public IHttpActionResult GetAllJournalsByTeacherId(string teacherId)
        {
            var result =_journalService.GetAllJournalsByTeacherId(teacherId);
            return Ok(result);
        }
    }
}
