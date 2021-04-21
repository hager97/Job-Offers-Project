namespace jobs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class complaint : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Complaints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Identity_User = c.String(nullable: false),
                        Message = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.AspNetUsers", "Statues", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Complaints", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Complaints", new[] { "UserId" });
            DropColumn("dbo.AspNetUsers", "Statues");
            DropTable("dbo.Complaints");
        }
    }
}
