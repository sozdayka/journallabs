using JournalLabs.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JournalLabs.API.ViewModels
{
    public class AddStudentToJournalViewModel
    {
        public string JournalId { get; set; }
        public List<Student> Students { get; set; }
    }
}