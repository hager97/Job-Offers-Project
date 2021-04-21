namespace jobs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifycol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Complaints", "Red_Email", c => c.String(nullable: false));
            DropColumn("dbo.Complaints", "Identity_User");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Complaints", "Identity_User", c => c.String(nullable: false));
            DropColumn("dbo.Complaints", "Red_Email");
        }
    }
}
