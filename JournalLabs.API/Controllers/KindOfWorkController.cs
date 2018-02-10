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
    [RoutePrefix("api/KindOfWork")]
    public class KindOfWorkController : ApiController
    {
        public KindOfWorkService _kindOfWorkService;
        public KindOfWorkController()
        {
            _kindOfWorkService = new KindOfWorkService();
        }
        [Route("GetKindOfWorks")]
        [HttpGet]
        public IHttpActionResult GetKindOfWorks()
        {
            return Ok("Good");
        }
        [Route("CreateKindOfWork")]
        [HttpPost]
        public IHttpActionResult CreateKindOfWork(KindOfWork kindOfWork)
        {
            _kindOfWorkService.CreateKindOfWork(kindOfWork);
            return Ok("Good");
        }
        [Route("UpdateKindOfWork")]
        [HttpPost]
        public IHttpActionResult UpdateKindOfWork(KindOfWork kindOfWork)
        {
            _kindOfWorkService.UpdateKindOfWork(kindOfWork);
            return Ok("Good");
        }
        [Route("GetKindOfWorkById")]
        [HttpGet]
        public IHttpActionResult GetKindOfWorkById(string Id)
        {
            var result = _kindOfWorkService.GetKindOfWorkById(Id);
            return Ok(result);
        }
        [Route("DeleteKindOfWorkById")]
        [HttpGet]
        public IHttpActionResult DeleteKindOfWorkById(string Id)
        {
            _kindOfWorkService.DeleteKindOfWorkById(Id);
            return Ok("Good");
        }
    }
}
