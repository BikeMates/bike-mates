namespace BikeMates.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsBanedForUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsBaned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsBaned");
        }
    }
}
