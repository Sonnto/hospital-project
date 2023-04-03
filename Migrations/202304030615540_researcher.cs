namespace hospital_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class researcher : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        department_id = c.Int(nullable: false, identity: true),
                        department_name = c.String(),
                    })
                .PrimaryKey(t => t.department_id);
            
            CreateTable(
                "dbo.Labs",
                c => new
                    {
                        LabId = c.Int(nullable: false, identity: true),
                        LabName = c.String(),
                    })
                .PrimaryKey(t => t.LabId);
            
            CreateTable(
                "dbo.Physicians",
                c => new
                    {
                        physician_id = c.Int(nullable: false, identity: true),
                        first_name = c.String(),
                        last_name = c.String(),
                        email = c.String(),
                    })
                .PrimaryKey(t => t.physician_id);
            
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
                        ResearchLeader = c.Boolean(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ResearcherId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Volunteers",
                c => new
                    {
                        volunteer_id = c.Int(nullable: false, identity: true),
                        first_name = c.String(),
                        last_name = c.String(),
                        email = c.String(),
                    })
                .PrimaryKey(t => t.volunteer_id);
            
          
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        doctor_id = c.Int(nullable: false, identity: true),
                        first_name = c.String(),
                        last_name = c.String(),
                        email = c.String(),
                    })
                .PrimaryKey(t => t.doctor_id);
            
            DropForeignKey("dbo.Researchers", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "LabId", "dbo.Labs");
            DropIndex("dbo.Researchers", new[] { "ProjectId" });
            DropIndex("dbo.Projects", new[] { "LabId" });
            DropTable("dbo.Volunteers");
            DropTable("dbo.Researchers");
            DropTable("dbo.Projects");
            DropTable("dbo.Physicians");
            DropTable("dbo.Labs");
            DropTable("dbo.Departments");
        }
    }
}
