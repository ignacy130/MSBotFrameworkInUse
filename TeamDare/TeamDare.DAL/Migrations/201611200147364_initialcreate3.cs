namespace TeamDare.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Adventures", "FinishedText", c => c.String());
            AddColumn("dbo.Adventures", "FinishedImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Adventures", "FinishedImageUrl");
            DropColumn("dbo.Adventures", "FinishedText");
        }
    }
}
