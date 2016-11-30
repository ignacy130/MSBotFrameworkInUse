namespace TeamDare.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adventures",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GameMasterId = c.Guid(nullable: false),
                        HeroId = c.Guid(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GameMasters", t => t.GameMasterId, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.HeroId)
                .Index(t => t.GameMasterId)
                .Index(t => t.HeroId);
            
            CreateTable(
                "dbo.Challenges",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AdventureId = c.Guid(nullable: false),
                        Title = c.String(),
                        IsCompleted = c.Boolean(nullable: false),
                        IsStarted = c.Boolean(nullable: false),
                        HeroId = c.Guid(nullable: false),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Adventures", t => t.AdventureId)
                .ForeignKey("dbo.Players", t => t.HeroId)
                .Index(t => t.AdventureId)
                .Index(t => t.HeroId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GameMasterId = c.Guid(nullable: false),
                        Level = c.Int(nullable: false),
                        Nick = c.String(),
                        AppNick = c.String(),
                        AppId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GameMasters", t => t.GameMasterId)
                .Index(t => t.GameMasterId);
            
            CreateTable(
                "dbo.GameMasters",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rewards",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PlayerId = c.Int(nullable: false),
                        AdventureId = c.Int(nullable: false),
                        Title = c.String(),
                        Value = c.Int(nullable: false),
                        Adventure_Id = c.Guid(),
                        Player_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Adventures", t => t.Adventure_Id)
                .ForeignKey("dbo.Players", t => t.Player_Id)
                .Index(t => t.Adventure_Id)
                .Index(t => t.Player_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rewards", "Player_Id", "dbo.Players");
            DropForeignKey("dbo.Rewards", "Adventure_Id", "dbo.Adventures");
            DropForeignKey("dbo.Adventures", "HeroId", "dbo.Players");
            DropForeignKey("dbo.Adventures", "GameMasterId", "dbo.GameMasters");
            DropForeignKey("dbo.Challenges", "HeroId", "dbo.Players");
            DropForeignKey("dbo.Players", "GameMasterId", "dbo.GameMasters");
            DropForeignKey("dbo.Challenges", "AdventureId", "dbo.Adventures");
            DropIndex("dbo.Rewards", new[] { "Player_Id" });
            DropIndex("dbo.Rewards", new[] { "Adventure_Id" });
            DropIndex("dbo.Players", new[] { "GameMasterId" });
            DropIndex("dbo.Challenges", new[] { "HeroId" });
            DropIndex("dbo.Challenges", new[] { "AdventureId" });
            DropIndex("dbo.Adventures", new[] { "HeroId" });
            DropIndex("dbo.Adventures", new[] { "GameMasterId" });
            DropTable("dbo.Rewards");
            DropTable("dbo.GameMasters");
            DropTable("dbo.Players");
            DropTable("dbo.Challenges");
            DropTable("dbo.Adventures");
        }
    }
}
