using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JournalLabs.API.ViewModels
{
    public class LabBlockViewModel
    {
        public Guid? Id { get; set; }
        public string FirstDate { get; set; }
        public int FirstMark { get; set; }
        public string SecondDate { get; set; }
        public int SecondMark { get; set; }
        public Guid StudentId { get; set; }
        public Guid KindOfWorkId { get; set; }
        public Guid JournalId { get; set; }
    }
}