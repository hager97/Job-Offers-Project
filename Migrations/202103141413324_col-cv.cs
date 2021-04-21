namespace jobs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class colcv : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplyForJobs", "CV", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplyForJobs", "CV");
        }
    }
}
