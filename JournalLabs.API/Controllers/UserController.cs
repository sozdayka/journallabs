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
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        public UserService _userService;
        public UserController()
        {
            _userService = new UserService();
        }
        [Route("GetUsers")]
        [HttpGet]
        public IHttpActionResult GetUsers()
        {
            return Ok("Good");
        }
        [Route("CreateUser")]
        [HttpPost]
        public IHttpActionResult CreateUser(User user)
        {
            _userService.CreateUser(user);
            return Ok("Good");
        }
        [Route("SignInUser")]
        [HttpPost]
        public IHttpActionResult SignInUser(User user)
        {
            var resultUser =_userService.SignInUser(user);
            if (resultUser==null)
            {
                return Ok();
            }
            return Ok(resultUser);
        }
        [Route("GetUserById")]
        [HttpGet]
        public IHttpActionResult GetUserById(string Id)
        {
            var result = _userService.GetUserById(Id);
            return Ok(result);
        }
        [Route("DeleteUserById")]
        [HttpGet]
        public IHttpActionResult DeleteUserById(string Id)
        {
            _userService.DeleteUserById(Id);
            return Ok("Good");
        }
        [Route("GetAllAssistants")]
        [HttpGet]
        public IHttpActionResult GetAllAssistants()
        {
            var result = _userService.GetAllAssistants();
            return Ok(result);
        }        
    }
}
