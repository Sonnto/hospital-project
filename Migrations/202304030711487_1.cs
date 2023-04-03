namespace hospital_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Labs", "Project_ProjectId", c => c.Int());
            CreateIndex("dbo.Labs", "Project_ProjectId");
            AddForeignKey("dbo.Labs", "Project_ProjectId", "dbo.Projects", "ProjectId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Labs", "Project_ProjectId", "dbo.Projects");
            DropIndex("dbo.Labs", new[] { "Project_ProjectId" });
            DropColumn("dbo.Labs", "Project_ProjectId");
        }
    }
}
