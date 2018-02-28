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
    [RoutePrefix("api/Cathedra")]
    public class CathedraController : ApiController
    {
        public CathedraService _cathedraService;
        public CathedraController()
        {
            _cathedraService = new CathedraService();
        }
        [Route("GetСathedras")]
        [HttpGet]
        public IHttpActionResult GetСathedras()
        {
            return Ok("Good");
        }
        [Route("CreateСathedra")]
        [HttpPost]
        public IHttpActionResult CreateСathedra(Cathedra cathedra)
        {
            _cathedraService.CreateCathedra(cathedra);
            return Ok("Good");
        }
        [Route("UpdateСathedra")]
        [HttpPost]
        public IHttpActionResult UpdateСathedra(Cathedra cathedra)
        {
            _cathedraService.UpdateCathedra(cathedra);
            return Ok("Good");
        }
        [Route("GetСathedraById")]
        [HttpGet]
        public IHttpActionResult GetСathedraById(string Id)
        {
            var result = _cathedraService.GetCathedraById(Id);
            return Ok(result);
        }
        [Route("DeleteСathedraById")]
        [HttpGet]
        public IHttpActionResult DeleteСathedraById(string Id)
        {
            _cathedraService.DeleteCathedraById(Id);
            return Ok("Good");
        }
    }
}
