using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JournalLabs.API.Models
{
    public class LabBlock
    {
        public Guid? Id { get; set; }
        public string FirstDate { get; set; }
        public int FirstMark { get; set; }
        public string SecondDate { get; set; }
        public int SecondMark { get; set; }
        public Guid StudentId { get; set; }
        public Guid KindOfWorkId { get; set; }
        public Guid JournalId { get; set; }
        public virtual Journal Journal { get; set; }
        public virtual Student Student { get; set; }
        public virtual KindOfWork KindOfWork { get; set; }
    }
}