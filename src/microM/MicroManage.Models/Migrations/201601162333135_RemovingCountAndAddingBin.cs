namespace MicroManage.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingCountAndAddingBin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductInventories", "BinId", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "Count");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Count", c => c.Int(nullable: false));
            DropColumn("dbo.ProductInventories", "BinId");
        }
    }
}
