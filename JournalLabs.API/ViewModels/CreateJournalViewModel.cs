﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JournalLabs.API.ViewModels
{
    public class CreateJournalViewModel
    {
        public string LessonName { get; set; }
        public int StudentsCount { get; set; }
        public int LabBlocksCount { get; set; }
        public List<Guid> TeacherIds { get; set; }
    }
}