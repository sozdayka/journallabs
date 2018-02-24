﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using JournalLabs.API.BLL;
using JournalLabs.API.Models;
using JournalLabs.API.ViewModels;

namespace JournalLabs.API.Controllers
{
    [RoutePrefix("api/Journal")]
    public class JournalController : ApiController
    {
        public JournalService _journalService;
        public JournalController()
        {
            _journalService = new JournalService();
        }
        [Route("GetJournals")]
        [HttpGet]
        public IHttpActionResult GetJournals()
        {           
            return Ok("Good");
        }
        [Route("CreateJournal")]
        [HttpPost]
        public IHttpActionResult CreateJournal(CreateJournalViewModel createJournalViewModel)
        {
             _journalService.CreateJournal(createJournalViewModel);
            return Ok("Good");
        }
        [Route("UpdateJournal")]
        [HttpPost]
        public IHttpActionResult UpdateJournal(Journal journal)
        {
            _journalService.UpdateJournal(journal);
            return Ok("Good");
        }
        [Route("GetJournalById")]
        [HttpGet]
        public IHttpActionResult GetJournalById(string Id,bool isTeacher)
        {
            var result = _journalService.GetJournalById(Id,isTeacher);
            return Ok(result);
        }
        [Route("GetJournalByIdAndStudentId")]
        [HttpGet]
        public IHttpActionResult GetJournalByIdAndStudentId(string journalId, string studentId, bool isTeacher)
        {
            var result = _journalService.GetJournalById(journalId, isTeacher, studentId);
            return Ok(result);
        }
        [Route("DeleteJournalById")]
        [HttpGet]
        public IHttpActionResult DeleteJournalById(string Id)
        {
            _journalService.DeleteJournalById(Id);
            return Ok("Good");
        }
        [Route("GetAllJournalsByTeacherId")]
        [HttpGet]
        public IHttpActionResult GetAllJournalsByTeacherId(string teacherId,string role)
        {
            if (role=="Teacher")
            {
                var teacherJournals = _journalService.GetAllJournalsByTeacherId(teacherId);
                return Ok(teacherJournals);
            }
            
            var assistantJournals = _journalService.GetAllJournalsByAssistantId(teacherId);
            return Ok(assistantJournals);
        }
        [Route("GetAllStudentJournalsByStudentName")]
        [HttpGet]
        public IHttpActionResult GetAllStudentJournalsByStudentName(string studentName)
        {
            var result = _journalService.GetAllStudentJournalsByStudentName(studentName);
            return Ok(result);
        }
        [Route("AddStudentToJournal")]
        [HttpGet]
        public IHttpActionResult AddStudentToJournal(string journalId)
        {
            _journalService.AddStudentToJournal(journalId);
            return Ok("Good");
        }
        [Route("AddKindOfWorkToJournal")]
        [HttpGet]
        public IHttpActionResult AddKindOfWorkToJournal(string journalId)
        {
            _journalService.AddKindOfWorkToJournal(journalId);
            return Ok("Good");
        }
    }
}
