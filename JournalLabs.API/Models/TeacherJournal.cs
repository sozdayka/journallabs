using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JournalLabs.API.Models
{
    public class TeacherJournal
    {
        public Guid? Id { get; set; }
        public Guid TeacherId { get; set; }
        public Guid JournalId { get; set; }
        public virtual Journal Journal { get; set; }
        public virtual User Teacher { get; set; }
    }
}