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
    //[EnableCors(origins: "http://localhost:54500", headers: "*", methods: "*")]
    [RoutePrefix("api/LabBlock")]
    public class LabBlockController : ApiController
    {
        public LabBlockService _labBlockService;
        public LabBlockController()
        {
            _labBlockService = new LabBlockService();
        }
        [Route("GetLabBlocks")]
        [HttpGet]
        public IHttpActionResult GetLabBlocks()
        {
            return Ok("Good");
        }
        [Route("CreateLabBlock")]
        [HttpPost]
        public IHttpActionResult CreateLabBlock(LabBlock labBlock)
        {
            _labBlockService.CreateLabBlock(labBlock);
            return Ok("Good");
        }
        [Route("UpdateLabBlock")]
        [HttpPost]
        public IHttpActionResult UpdateLabBlock(LabBlock labBlock)
        {
            _labBlockService.UpdateLabBlock(labBlock);
            return Ok("Good");
        }
        [Route("GetLabBlockById")]
        [HttpGet]
        public IHttpActionResult GetLabBlockById(string Id)
        {
            var result = _labBlockService.GetLabBlockById(Id);
            return Ok(result);
        }
        [Route("DeleteLabBlockByStudentId")]
        [HttpGet]
        public IHttpActionResult DeleteLabBlockByStudentId(string studentId)
        {
            _labBlockService.DeleteLabBlockByStudentId(studentId);
            return Ok("Good");
        }       
        
    }
}
