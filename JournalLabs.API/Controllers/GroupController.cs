using JournalLabs.API.BLL;
using JournalLabs.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JournalLabs.API.Controllers
{
    [RoutePrefix("api/Group")]
    public class GroupController : ApiController
    {
        public GroupService _groupService;
        public GroupController()
        {
            _groupService = new GroupService();
        }
        [Route("GetGroups")]
        [HttpGet]
        public IHttpActionResult GetGroups()
        {
            var groups = _groupService.GetGroups();
            return Ok(groups);
        }
        [Route("CreateGroup")]
        [HttpPost]
        public IHttpActionResult CreateGroup(Group group)
        {
            _groupService.CreateGroup(group);
            return Ok("Good");
        }
        [Route("UpdateGroup")]
        [HttpPost]
        public IHttpActionResult UpdateGroup(Group group)
        {
            _groupService.UpdateGroup(group);
            return Ok("Good");
        }
        [Route("GetGroupById")]
        [HttpGet]
        public IHttpActionResult GetGroupById(string Id)
        {
            var result = _groupService.GetGroupById(Id);
            return Ok(result);
        }
        [Route("DeleteGroupById")]
        [HttpGet]
        public IHttpActionResult DeleteGroupById(string Id)
        {
            _groupService.DeleteGroupById(Id);
            return Ok("Good");
        }
    }
}
