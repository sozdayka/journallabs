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
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        public UserService _userService;
        public UserController()
        {
            _userService = new UserService();
        }
        [Route("GetUsers")]
        public IHttpActionResult GetUsers()
        {
            return Ok("Good");
        }
        [Route("CreateUser")]
        public IHttpActionResult CreateUser(User user)
        {
            _userService.CreateUser(user);
            return Ok("Good");
        }
        [Route("UpdateUser")]
        public IHttpActionResult UpdateUser(User user)
        {
            _userService.UpdateUser(user);
            return Ok("Good");
        }
        [Route("GetUserById")]
        public IHttpActionResult GetUserById(string Id)
        {
            var result = _userService.GetUserById(Id);
            return Ok(result);
        }
        [Route("DeleteUserById")]
        public IHttpActionResult DeleteUserById(string Id)
        {
            _userService.DeleteUserById(Id);
            return Ok("Good");
        }
    }
}
