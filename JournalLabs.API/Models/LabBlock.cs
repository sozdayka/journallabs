using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JournalLabs.API.Models
{
    public class LabBlock
    {
        public Guid Id { get; set; }
        public DateTime FirstDate { get; set; }
        public int FirstMark { get; set; }
        public DateTime SecondDate { get; set; }
        public int SecondMark { get; set; }
        public string UserFIO { get; set; }
        public Guid LessonId { get; set; }
        public Guid JournalId { get; set; }
        public virtual Lesson Lesson { get; set; }
        public virtual Journal Journal { get; set; }
    }
}