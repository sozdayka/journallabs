using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JournalLabs.API.ViewModels
{
    public class JournalViewModel
    {
        public Guid? Id { get; set; }
        public bool IsExam { get; set; }
        public string GroupName { get; set; }
        public string LessonName { get; set; }
    }
}