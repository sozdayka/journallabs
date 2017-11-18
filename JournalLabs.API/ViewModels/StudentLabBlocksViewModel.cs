using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JournalLabs.API.Models;

namespace JournalLabs.API.ViewModels
{
    public class StudentLabBlocksViewModel
    {
        public Student StudentInfo { get; set; }
        public List<LabBlockViewModel> StudentLabBlocks { get; set; }
    }
}