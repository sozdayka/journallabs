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
    [RoutePrefix("api/LabBlock")]
    public class LabBlockController : ApiController
    {
        public LabBlockService _labBlockService;
        public LabBlockController()
        {
            _labBlockService = new LabBlockService();
        }
        [Route("GetLabBlocks")]
        public IHttpActionResult GetJournals()
        {
            return Ok("Good");
        }
        [Route("CreateLabBlock")]
        public IHttpActionResult CreateLabBlock(LabBlock labBlock)
        {
            return Ok("Good");
        }
        [Route("UpdateLabBlock")]
        public IHttpActionResult UpdateLabBlock(LabBlock labBlock)
        {
            return Ok("Good");
        }
        [Route("GetLabBlockById")]
        public IHttpActionResult GetLabBlockById(string Id)
        {
            return Ok("Good");
        }
        [Route("DeleteLabBlockById")]
        public IHttpActionResult DeleteLabBlockById(string Id)
        {
            return Ok("Good");
        }
    }
}
