using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JournalLabs.API.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        public string StudentName { get; set; }
    }
}