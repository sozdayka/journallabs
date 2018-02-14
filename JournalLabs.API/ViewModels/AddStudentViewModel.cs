using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JournalLabs.API.ViewModels
{
    public class AddStudentViewModel
    {
        public string JournalId { get; set; }
        public string StudentName { get; set; }
        public List<LabBlockViewModel> LabBlocksSettings { get; set; }
    }
}