using JournalLabs.API.BLL.Provider;
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
        FileProvider fileFrovider;
        public LogController()
        {
            fileFrovider=new FileProvider();
        }
        [Route("WriteTeacherLog")]
        [HttpGet]
        public IHttpActionResult WriteTeacherLog(string data)
        {
            fileFrovider.Write("TeacherLog.txt", data);
            return Ok("Good");
        }
        [Route("WriteDevelopmentLog")]
        [HttpGet]
        public IHttpActionResult WriteDevelopmentLog(string data)
        {
            fileFrovider.Write("DevelopmentLog.txt", data);
            return Ok("Good");
        }
    }
}
