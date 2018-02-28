using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JournalLabs.API.Models
{
    public class Cathedra
    {
        public Guid Id { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
    }
}