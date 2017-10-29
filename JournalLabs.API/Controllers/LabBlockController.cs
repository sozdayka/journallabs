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
        public IHttpActionResult GetLabBlocks()
        {
            return Ok("Good");
        }
        [Route("CreateLabBlock")]
        public IHttpActionResult CreateLabBlock(LabBlock labBlock)
        {
            _labBlockService.CreateLabBlock(labBlock);
            return Ok("Good");
        }
        [Route("UpdateLabBlock")]
        public IHttpActionResult UpdateLabBlock(LabBlock labBlock)
        {
            _labBlockService.UpdateLabBlock(labBlock);
            return Ok("Good");
        }
        [Route("GetLabBlockById")]
        public IHttpActionResult GetLabBlockById(string Id)
        {
            var result = _labBlockService.GetLabBlockById(Id);
            return Ok(result);
        }
        [Route("DeleteLabBlockById")]
        public IHttpActionResult DeleteLabBlockById(string Id)
        {
            _labBlockService.DeleteLabBlockById(Id);
            return Ok("Good");
        }
    }
}
