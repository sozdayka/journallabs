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
    public class UserController : ApiController
    {
        public UserService _userService;
        public UserController()
        {
            _userService = new UserService();
        }
        [Route("GetUsers")]
        public IHttpActionResult GetJournals()
        {
            return Ok("Good");
        }
        [Route("CreateUser")]
        public IHttpActionResult CreateUser(User user)
        {
            return Ok("Good");
        }
        [Route("UpdateUser")]
        public IHttpActionResult UpdateUser(User user)
        {
            return Ok("Good");
        }
        [Route("GetUserById")]
        public IHttpActionResult GetUserById(string Id)
        {
            return Ok("Good");
        }
        [Route("DeleteUserById")]
        public IHttpActionResult DeleteUserById(string Id)
        {
            return Ok("Good");
        }
    }
}
