using JournalLabs.API.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JournalLabs.API.ViewModels
{
    public class LabBlockViewModel
    {
        public Guid? Id { get; set; }
        public string Date { get; set; }
        public double Mark { get; set; }
        public Guid? MarkTeacherId { get; set; }
        public KindOfMark KindOfMark { get; set; }
        public Guid StudentId { get; set; }
        public Guid KindOfWorkId { get; set; }
        public Guid JournalId { get; set; }
        public bool IsKindOfWorkVisible { get; set; }
        public bool IsCalculateMark { get; set; }
        public bool IsVisibleToStudent { get; set; }
        public bool IsBoolField { get; set; }
        public bool IsSecondBlock { get; set; }
    }
}