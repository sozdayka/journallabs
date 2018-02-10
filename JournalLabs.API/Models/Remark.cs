using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JournalLabs.API.Models
{
    public class Remark
    {
        public Guid? Id { get; set; }
        public bool IsHideStudent { get; set; }
        public string RemarkText { get; set; }
        public Guid StudentId { get; set; }
        public Guid JournalId { get; set; }
        public virtual Journal Journal { get; set; }
        public virtual Student Student { get; set; }
    }
}