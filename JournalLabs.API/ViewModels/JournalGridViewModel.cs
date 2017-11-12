using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JournalLabs.API.Models;

namespace JournalLabs.API.ViewModels
{
    public class JournalGridViewModel
    {
        public JournalViewModel JournalModel { get; set; }
        public List<KindOfWork> KindsOfWorkForJournal { get; set; }
        public List<StudentJournalViewModel> StudentResultForJournal { get; set; }
    }
}