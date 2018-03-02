using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JournalLabs.API.Models
{
    public class StudentGroup
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid GroupId { get; set; }
        public virtual Student Student { get; set; }
        public virtual Group Group { get; set; }
    }
}