using JournalLabs.API.BLL;
using JournalLabs.API.BLL.Provider;
using JournalLabs.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JournalLabs.API.Controllers
{
    [RoutePrefix("api/Log")]
    public class LogController : ApiController
    {
        public LogService _logService;
        public LogController()
        {
            _logService = new LogService();
        }
        [Route("CreateLog")]
        [HttpPost]
        public IHttpActionResult CreateLog(Log logModel)
        {
            _logService.CreateLog(logModel);
            return Ok("Good");
        }
        [Route("GetLogsByType")]
        [HttpGet]
        public IHttpActionResult GetLogsByType(string type)
        {
            var result = _logService.GetLogsByType(type);
            return Ok("Good");
        }
    }
}
