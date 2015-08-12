namespace MailingListApp.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MailingListApp.Models.MailingListAppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MailingListApp.Models.MailingListAppContext";
        }

        protected override void Seed(MailingListApp.Models.MailingListAppContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.MailingLists.AddOrUpdate(
              x => x.Id,
              new MailingList { Id = 1, Name = "List1", Description = "List 1 description" },
              new MailingList { Id = 2, Name = "List2", Description = "List 2 description" },
              new MailingList { Id = 3, Name = "List3", Description = "List 3 description" }
            );
        }
    }
}
