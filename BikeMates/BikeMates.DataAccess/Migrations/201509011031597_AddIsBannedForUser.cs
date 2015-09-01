namespace BikeMates.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsBannedForUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsBanned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsBanned");
        }
    }
}
