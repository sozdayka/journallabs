namespace JournalLabs.API.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<JournalLabs.API.DAL.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(JournalLabs.API.DAL.ApplicationContext context)
        {
            context.Users.AddOrUpdate(x => x.Id, new Models.User { Id = Guid.NewGuid(), Login = "admin", Password = "111111", Role = "Admin" });
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
