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
                                            new Customer { Id = 2, MailToAddressId = 3, ShipToAddressId = 4, FirstName = "Andrew", LastName = "Fuller" },
                                            new Customer { Id = 3, MailToAddressId = 5, ShipToAddressId = 6, FirstName = "Janet", LastName = "Leverling" },
                                            new Customer { Id = 4, MailToAddressId = 7, ShipToAddressId = 8, FirstName = "Margaret", LastName = "Peacock" },
                                            new Customer { Id = 5, MailToAddressId = 9, ShipToAddressId = 10, FirstName = "Steven", LastName = "Buchanan" },
                                            new Customer { Id = 6, MailToAddressId = 11, ShipToAddressId = 12, FirstName = "Michael", LastName = "Suyama" },
                                            new Customer { Id = 7, MailToAddressId = 13, ShipToAddressId = 14, FirstName = "Robert", LastName = "King" },
                                            new Customer { Id = 8, MailToAddressId = 15, ShipToAddressId = 16, FirstName = "Laura", LastName = "Callahan" },
                                            new Customer { Id = 9, MailToAddressId = 17, ShipToAddressId = 18, FirstName = "Anne", LastName = "Dodsworth" });
            context.Addresses.AddOrUpdate(  new Address { Id = 1, CustomerID = 1, Street = "23 Tsawassen Blvd.", City = "Tsawassen", State = "BC", PostalCode = "T2F 8M4" },
                                            new Address { Id = 2, CustomerID = 1, Street = "Av. dos Lusíadas, 23", City = "Sao Paulo", State = "SP", PostalCode = "05432-043" },
                                            new Address { Id = 3, CustomerID = 2, Street = "Rua Orós, 92", City = "Sao Paulo", State = "SP", PostalCode = "05442-030" },
                                            new Address { Id = 4, CustomerID = 2, Street = "Av. Brasil, 442", City = "Campinas", State = "SP", PostalCode = "04876-786" },
                                            new Address { Id = 5, CustomerID = 3, Street = "2732 Baker Blvd.", City = "Eugene", State = "OR", PostalCode = "97403" },
                                            new Address { Id = 6, CustomerID = 3, Street = "Rua do Paço, 67", City = "Rio de Janeiro", State = "RJ", PostalCode = "05454-876" },
                                            new Address { Id = 7, CustomerID = 4, Street = "City Center Plaza 516 Main St.", City = "Elgin", State = "OR", PostalCode = "97827" },
                                            new Address { Id = 8, CustomerID = 4, Street = "1900 Oak St.", City = "Vancouver", State = "BC", PostalCode = "V3F 2K1" },
                                            new Address { Id = 9, CustomerID = 5, Street = "12 Orchestra Terrace", City = "Walla Walla", State = "WA", PostalCode = "99362" },
                                            new Address { Id = 10, CustomerID = 5, Street = "87 Polk St. Suite 5", City = "San Francisco", State = "CA", PostalCode = "94117" },
                                            new Address { Id = 11, CustomerID = 6, Street = "89 Chiaroscuro Rd.", City = "Portland", State = "OR", PostalCode = "97219" },
                                            new Address { Id = 12, CustomerID = 6, Street = "43 rue St. Laurent", City = "Montréal", State = "Québec", PostalCode = "H1J 1C3" },
                                            new Address { Id = 13, CustomerID = 7, Street = "2743 Bering St.", City = "Anchorage", State = "AK", PostalCode = "99508" },
                                            new Address { Id = 14, CustomerID = 7, Street = "Rua da Panificadora, 12", City = "Rio de Janeiro", State = "RJ", PostalCode = "02389-673" },
                                            new Address { Id = 15, CustomerID = 8, Street = "Alameda dos Canàrios, 891", City = "Sao Paulo", State = "SP", PostalCode = "05487-020" },
                                            new Address { Id = 16, CustomerID = 8, Street = "2817 Milton Dr.", City = "Albuquerque", State = "NM", PostalCode = "87110" },
                                            new Address { Id = 17, CustomerID = 9, Street = "Av. Copacabana, 267", City = "Rio de Janeiro", State = "RJ", PostalCode = "02389-890" },
                                            new Address { Id = 18, CustomerID = 9, Street = "187 Suffolk Ln.", City = "Boise", State = "ID", PostalCode = "83720" });

            context.Products.AddOrUpdate(   new Product { Id = 1, Name = "Engine", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque ligula nisi, efficitur sed scelerisque sed, laoreet sit amet magna. Integer porttitor lacus sit amet aliquam volutpat. Donec ullamcorper orci id malesuada sodales. Integer egestas, odio in porttitor tincidunt, purus magna dignissim justo, non lacinia nibh odio eu lorem. Ut dapibus posuere facilisis. Quisque quis dolor a mi ullamcorper maximus et sed enim.", WebAvailable = true, Price = 4000 },
                                            new Product { Id = 2, Name = "Transmission", Description = "Nulla faucibus, nunc quis hendrerit fringilla, urna erat imperdiet ex, vel tristique odio quam at augue. Fusce faucibus odio volutpat lacus accumsan tristique. Duis vestibulum orci ac ex ullamcorper bibendum. Nulla et ipsum enim. Proin quis nisl sagittis, placerat est non, bibendum mauris. ", WebAvailable = true, Price = 4000 },
                                            new Product { Id = 3, Name = "Chassis", Description = "Vivamus at velit vel ante aliquam ullamcorper. Ut laoreet risus varius ultricies porttitor. Proin condimentum magna tortor, et condimentum est feugiat id. Aliquam placerat nisl et risus pretium cursus. Fusce in odio luctus, lobortis arcu vel, gravida urna. Nullam eleifend suscipit ", WebAvailable = true, Price = 14000 },
                                            new Product { Id = 4, Name = "Suspension", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque ligula nisi, efficitur sed scelerisque sed, laoreet sit amet magna. Integer porttitor lacus sit amet aliquam volutpat. Donec ullamcorper orci id malesuada sodales. Integer egestas, odio in porttitor tincidunt, purus magna dignissim justo, non lacinia nibh odio eu lorem. Ut dapibus posuere facilisis. Quisque quis dolor a mi ullamcorper maximus et sed enim.", WebAvailable = true, Price = 210 },
                                            new Product { Id = 5, Name = "Shock Absorbers", Description = "Nulla faucibus, nunc quis hendrerit fringilla, urna erat imperdiet ex, vel tristique odio quam at augue. Fusce faucibus odio volutpat lacus accumsan tristique. Duis vestibulum orci ac ex ullamcorper bibendum. Nulla et ipsum enim. Proin quis nisl sagittis, placerat est non, bibendum mauris. ", WebAvailable = true, Price = 100 },
                                            new Product { Id = 6, Name = "Coil Spring", Description = "Vivamus at velit vel ante aliquam ullamcorper. Ut laoreet risus varius ultricies porttitor. Proin condimentum magna tortor, et condimentum est feugiat id. Aliquam placerat nisl et risus pretium cursus. Fusce in odio luctus, lobortis arcu vel, gravida urna. Nullam eleifend suscipit ", WebAvailable = true, Price = 120 },
                                            new Product { Id = 7, Name = "Engine Bracket", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque ligula nisi, efficitur sed scelerisque sed, laoreet sit amet magna. Integer porttitor lacus sit amet aliquam volutpat. Donec ullamcorper orci id malesuada sodales. Integer egestas, odio in porttitor tincidunt, purus magna dignissim justo, non lacinia nibh odio eu lorem. Ut dapibus posuere facilisis. Quisque quis dolor a mi ullamcorper maximus et sed enim.", WebAvailable = true, Price = 20 },
                                            new Product { Id = 8, Name = "Car", Description = "Nulla faucibus, nunc quis hendrerit fringilla, urna erat imperdiet ex, vel tristique odio quam at augue. Fusce faucibus odio volutpat lacus accumsan tristique. Duis vestibulum orci ac ex ullamcorper bibendum. Nulla et ipsum enim. Proin quis nisl sagittis, placerat est non, bibendum mauris. ", WebAvailable = true, Price = 20000 },
                                            new Product { Id = 9, Name = "Truck", Description = "Vivamus at velit vel ante aliquam ullamcorper. Ut laoreet risus varius ultricies porttitor. Proin condimentum magna tortor, et condimentum est feugiat id. Aliquam placerat nisl et risus pretium cursus. Fusce in odio luctus, lobortis arcu vel, gravida urna. Nullam eleifend suscipit ", WebAvailable = true, Price = 40000 },
                                            new Product { Id = 10, Name = "Tie Fighter", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque ligula nisi, efficitur sed scelerisque sed, laoreet sit amet magna. Integer porttitor lacus sit amet aliquam volutpat. Donec ullamcorper orci id malesuada sodales. Integer egestas, odio in porttitor tincidunt, purus magna dignissim justo, non lacinia nibh odio eu lorem. Ut dapibus posuere facilisis. Quisque quis dolor a mi ullamcorper maximus et sed enim.", WebAvailable = true, Price = 200000 },
                                            new Product { Id = 11, Name = "Bus", Description = "Nulla faucibus, nunc quis hendrerit fringilla, urna erat imperdiet ex, vel tristique odio quam at augue. Fusce faucibus odio volutpat lacus accumsan tristique. Duis vestibulum orci ac ex ullamcorper bibendum. Nulla et ipsum enim. Proin quis nisl sagittis, placerat est non, bibendum mauris. ", WebAvailable = true, Price = 90000 },
                                            new Product { Id = 12, Name = "Boat", Description = "Vivamus at velit vel ante aliquam ullamcorper. Ut laoreet risus varius ultricies porttitor. Proin condimentum magna tortor, et condimentum est feugiat id. Aliquam placerat nisl et risus pretium cursus. Fusce in odio luctus, lobortis arcu vel, gravida urna. Nullam eleifend suscipit ", WebAvailable = true, Price = 80000 });

            context.Categories.AddOrUpdate(new Category { Id = 1, Name = "Main", IsActive = true });
        }
    }
}
