namespace MicroManage.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Street = c.String(),
                        City = c.String(),
                        State = c.String(),
                        PostalCode = c.String(),
                        CustomerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: false)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MailToAddressId = c.Int(nullable: false),
                        ShipToAddressId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.MailToAddressId, cascadeDelete: false)
                .ForeignKey("dbo.Addresses", t => t.ShipToAddressId, cascadeDelete: false)
                .Index(t => t.MailToAddressId)
                .Index(t => t.ShipToAddressId);
            
            CreateTable(
                "dbo.BinRFIDAssignments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BinAddress = c.String(),
                        BinId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bins", t => t.BinId, cascadeDelete: true)
                .Index(t => t.BinId);
            
            CreateTable(
                "dbo.Bins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Location = c.String(),
                        Site = c.String(),
                        AssignedProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InventoryAudits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InventoryId = c.Int(nullable: false),
                        EventDate = c.DateTime(nullable: false),
                        OldStatus = c.Int(nullable: false),
                        NewStatus = c.Int(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductInventories", t => t.InventoryId, cascadeDelete: true)
                .Index(t => t.InventoryId);
            
            CreateTable(
                "dbo.ProductInventories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        SerialId = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        WebAvailable = c.Boolean(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.InventoryAudits", "InventoryId", "dbo.ProductInventories");
            DropForeignKey("dbo.BinRFIDAssignments", "BinId", "dbo.Bins");
            DropForeignKey("dbo.Addresses", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.Customers", "ShipToAddressId", "dbo.Addresses");
            DropForeignKey("dbo.Customers", "MailToAddressId", "dbo.Addresses");
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.InventoryAudits", new[] { "InventoryId" });
            DropIndex("dbo.BinRFIDAssignments", new[] { "BinId" });
            DropIndex("dbo.Customers", new[] { "ShipToAddressId" });
            DropIndex("dbo.Customers", new[] { "MailToAddressId" });
            DropIndex("dbo.Addresses", new[] { "CustomerID" });
            DropTable("dbo.Products");
            DropTable("dbo.Orders");
            DropTable("dbo.ProductInventories");
            DropTable("dbo.InventoryAudits");
            DropTable("dbo.Bins");
            DropTable("dbo.BinRFIDAssignments");
            DropTable("dbo.Customers");
            DropTable("dbo.Addresses");
        }
    }
}
