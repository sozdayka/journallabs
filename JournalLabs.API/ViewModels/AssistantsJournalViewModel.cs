using JournalLabs.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JournalLabs.API.ViewModels
{
    public class AssistantsJournalViewModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public bool IsAllowAccess { get; set; }
        public Guid? TeacherJournalId { get; set; }
    }
}