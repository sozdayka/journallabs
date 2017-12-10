using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JournalLabs.API.ViewModels
{
    public class RemarkViewModel
    {
        public Guid? Id { get; set; }
        public string RemarkText { get; set; }
        public Guid StudentId { get; set; }
        public Guid JournalId { get; set; }
    }
}