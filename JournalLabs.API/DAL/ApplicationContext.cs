using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using JournalLabs.API.Models;

namespace JournalLabs.API.DAL
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext():base("DefaultConnection")
        {
            
        }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<LabBlock> LabBlocks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<KindOfWork> KindOfWorks { get; set; }
        public DbSet<Student> Students { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}