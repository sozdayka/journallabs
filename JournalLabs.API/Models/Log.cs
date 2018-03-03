using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JournalLabs.API.Models
{
    public class Log
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
    }
}