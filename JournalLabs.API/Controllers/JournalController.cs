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
    [RoutePrefix("api/Journal")]
    public class JournalController : ApiController
    {
        public JournalService _journalService;
        public JournalController()
        {
            _journalService = new JournalService();
        }
        [Route("GetJournals")]
        public IHttpActionResult GetJournals()
        {           
            return Ok("Good");
        }
        [Route("CreateJournal")]
        public IHttpActionResult CreateJournal(Journal journal)
        {
             _journalService.CreateJournal(journal);
            return Ok("Good");
        }
        [Route("UpdateJournal")]
        public IHttpActionResult UpdateJournal(Journal journal)
        {
            _journalService.UpdateJournal(journal);
            return Ok("Good");
        }
        [Route("GetJournalById")]
        public IHttpActionResult GetJournalById(string Id)
        {
            var result = _journalService.GetJournalById(Id);
            return Ok(result);
        }
        [Route("DeleteJournalById")]
        public IHttpActionResult DeleteJournalById(string Id)
        {
            _journalService.DeleteJournalById(Id);
            return Ok("Good");
        }
    }
}
