namespace MicroManage.Models.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MicroManage.Models.MicroManageContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MicroManage.Models.MicroManageContext context)
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
            context.Customers.AddOrUpdate(  new Customer { Id = 1, MailToAddressId = 1, ShipToAddressId = 2, FirstName = "Nancy", LastName = "Davolio" },
            context.Addresses.AddOrUpdate(  new Address { Id = 1, CustomerID = 1, Street = "23 Tsawassen Blvd.", City = "Tsawassen", State = "BC", PostalCode = "T2F 8M4" },
        }
    }
}