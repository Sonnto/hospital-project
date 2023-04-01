namespace hospital_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Projects : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "LabId", "dbo.Labs");
            DropIndex("dbo.Projects", new[] { "LabId" });
            DropTable("dbo.Projects");
        }
    }
}
