namespace hospital_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Researcher : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Researchers",
                c => new
                    {
                        ResearcherId = c.Int(nullable: false, identity: true),
                        ResearcherName = c.String(),
                        ProjectId = c.Int(nullable: false),
                        ResearchLeader = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ResearcherId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Researchers");
        }
    }
}
