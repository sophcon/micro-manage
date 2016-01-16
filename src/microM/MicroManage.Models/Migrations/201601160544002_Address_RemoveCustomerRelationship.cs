namespace MicroManage.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Address_RemoveCustomerRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Addresses", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Addresses", new[] { "CustomerID" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Addresses", "CustomerID");
            AddForeignKey("dbo.Addresses", "CustomerID", "dbo.Customers", "Id", cascadeDelete: true);
        }
    }
}
