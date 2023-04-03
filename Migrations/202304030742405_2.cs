namespace hospital_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Lab_LabId", c => c.Int());
            CreateIndex("dbo.Projects", "Lab_LabId");
            AddForeignKey("dbo.Projects", "Lab_LabId", "dbo.Labs", "LabId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "Lab_LabId", "dbo.Labs");
            DropIndex("dbo.Projects", new[] { "Lab_LabId" });
            DropColumn("dbo.Projects", "Lab_LabId");
        }
    }
}
