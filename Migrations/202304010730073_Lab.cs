namespace hospital_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lab : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Labs",
                c => new
                    {
                        LabId = c.Int(nullable: false, identity: true),
                        LabName = c.String(),
                    })
                .PrimaryKey(t => t.LabId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(),
                        LabId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.Labs", t => t.LabId, cascadeDelete: true)
                .Index(t => t.LabId);
            
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
            DropForeignKey("dbo.Projects", "LabId", "dbo.Labs");
            DropIndex("dbo.Projects", new[] { "LabId" });
            DropTable("dbo.Researchers");
            DropTable("dbo.Projects");
            DropTable("dbo.Labs");
        }
    }
}
