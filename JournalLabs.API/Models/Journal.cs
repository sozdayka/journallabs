using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JournalLabs.API.Models
{
    public class Journal
    {
        public Guid? Id { get; set; }
        public string LessonName { get; set; }
        public virtual List<LabBlock> LabBlocks { get; set; }
    }
}