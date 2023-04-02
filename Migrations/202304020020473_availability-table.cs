namespace hospital_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class availabilitytable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Availabilities",
                c => new
                    {
                        availability_id = c.Int(nullable: false, identity: true),
                        physician_id = c.Int(nullable: false),
                        department_id = c.Int(nullable: false),
                        availability_dates = c.String(),
                    })
                .PrimaryKey(t => t.availability_id)
                .ForeignKey("dbo.Departments", t => t.department_id, cascadeDelete: true)
                .ForeignKey("dbo.Physicians", t => t.physician_id, cascadeDelete: true)
                .Index(t => t.physician_id)
                .Index(t => t.department_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Availabilities", "physician_id", "dbo.Physicians");
            DropForeignKey("dbo.Availabilities", "department_id", "dbo.Departments");
            DropIndex("dbo.Availabilities", new[] { "department_id" });
            DropIndex("dbo.Availabilities", new[] { "physician_id" });
            DropTable("dbo.Availabilities");
        }
    }
}
