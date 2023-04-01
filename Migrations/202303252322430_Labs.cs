namespace hospital_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Labs : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Labs");
        }
    }
}
