using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JournalLabs.API.Models;

namespace JournalLabs.API.ViewModels
{
    public class StudentJournal
    {
        public Guid StudentId { get; set; }
        public Guid JournalId { get; set; }
        public string LessonName { get; set; }
    }
}