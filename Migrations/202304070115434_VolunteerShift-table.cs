namespace hospital_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VolunteerShifttable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VolunteerShifts",
                c => new
                    {
                        shift_id = c.Int(nullable: false, identity: true),
                        volunteer_id = c.Int(nullable: false),
                        department_id = c.Int(nullable: false),
                        date = c.String(),
                    })
                .PrimaryKey(t => t.shift_id)
                .ForeignKey("dbo.Departments", t => t.department_id, cascadeDelete: true)
                .ForeignKey("dbo.Volunteers", t => t.volunteer_id, cascadeDelete: true)
                .Index(t => t.volunteer_id)
                .Index(t => t.department_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VolunteerShifts", "volunteer_id", "dbo.Volunteers");
            DropForeignKey("dbo.VolunteerShifts", "department_id", "dbo.Departments");
            DropIndex("dbo.VolunteerShifts", new[] { "department_id" });
            DropIndex("dbo.VolunteerShifts", new[] { "volunteer_id" });
            DropTable("dbo.VolunteerShifts");
        }
    }
}
