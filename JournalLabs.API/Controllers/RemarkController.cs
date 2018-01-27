﻿using JournalLabs.API.BLL;
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
    [EnableCors(origins: "http://localhost:62106", headers: "*", methods: "*")]
    [RoutePrefix("api/Remark")]
    public class RemarkController : ApiController
    {
        public RemarkService _remarkService;
        public RemarkController()
        {
            _remarkService = new RemarkService();
        }
        [Route("UpdateRemark")]
        [HttpPost]
        public IHttpActionResult UpdateRemark(Remark remark)
        {
            _remarkService.UpdateRemark(remark);
            return Ok("Good");
        }
    }
}