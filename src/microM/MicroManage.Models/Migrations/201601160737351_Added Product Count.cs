namespace MicroManage.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProductCount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Count", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Count");
        }
    }
}
