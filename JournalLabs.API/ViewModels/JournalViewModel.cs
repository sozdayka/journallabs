using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JournalLabs.API.ViewModels
{
    public class JournalViewModel
    {
        public Guid? Id { get; set; }
        public Guid TeacherId { get; set; }
        public string LessonName { get; set; }
    }
}