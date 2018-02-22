using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JournalLabs.API.Models
{
    public class KindOfWork
    {
        public Guid Id { get; set; }
        public string NameKindOfWork { get; set; }
        public bool IsKindOfWorkVisible { get; set; }
        public bool IsVisibleToStudent { get; set; }
        public bool IsSecondBlock { get; set; }
    }
}