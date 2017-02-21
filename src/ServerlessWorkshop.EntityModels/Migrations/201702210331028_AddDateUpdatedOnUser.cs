namespace ServerlessWorkshop.SampleDbContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateUpdatedOnUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "DateUpdated", c => c.DateTimeOffset(precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "DateUpdated");
        }
    }
}
