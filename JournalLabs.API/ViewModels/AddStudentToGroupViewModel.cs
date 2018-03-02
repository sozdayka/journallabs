using JournalLabs.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JournalLabs.API.ViewModels
{
    public class AddStudentToGroupViewModel
    {
        public Guid? Id { get; set; }
        public Guid GroupId { get; set; }
        public Student Student { get; set; }
    }
}