using JournalLabs.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JournalLabs.API.ViewModels
{
    public class CreateJournalViewModel
    {
        public string LessonName { get; set; }
        public bool IsExam { get; set; }
        public string GroupName { get; set; }
        public List<Student> Students { get; set; }
        public List<LabBlockViewModel> LabBlocksSettings { get; set; }
        public List<Guid> TeacherIds { get; set; }
    }
}