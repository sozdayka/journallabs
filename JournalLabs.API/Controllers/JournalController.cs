using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JournalLabs.API.Controllers
{
    [RoutePrefix("api/Journal")]
    public class JournalController : ApiController
    {
        [Route("GetJournals")]
        public IHttpActionResult GetJournals()
        {
            return Ok("Good");
        }
    }
}
