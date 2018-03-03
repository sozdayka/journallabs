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
        [Route("GetCathedras")]
        [HttpGet]
        public IHttpActionResult GetCathedras()
        {
            var cathedras = _cathedraService.GetCathedras();
            return Ok(cathedras);
        }
        [Route("CreateCathedra")]
        [HttpPost]
        public IHttpActionResult CreateCathedra(Cathedra cathedra)
        {
            _cathedraService.CreateCathedra(cathedra);
            return Ok("Good");
        }
        
        [Route("UpdateCathedra")]
        [HttpPost]
        public IHttpActionResult UpdateCathedra(Cathedra cathedra)
        {
            _cathedraService.UpdateCathedra(cathedra);
            return Ok("Good");
        }
        [Route("GetCathedraById")]
        [HttpGet]
        public IHttpActionResult GetСathedraById(string Id)
        {
            var result = _cathedraService.GetCathedraById(Id);
            return Ok(result);
        }
        [Route("DeleteCathedraById")]
        [HttpGet]
        public IHttpActionResult DeleteCathedraById(string Id)
        {
            _cathedraService.DeleteCathedraById(Id);
            return Ok("Good");
        }
    }
}
