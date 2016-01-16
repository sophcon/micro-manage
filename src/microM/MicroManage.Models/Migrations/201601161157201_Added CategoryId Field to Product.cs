namespace MicroManage.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCategoryIdFieldtoProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "CategoryId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "CategoryId");
        }
    }
}
