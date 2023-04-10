namespace hospital_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class philantropistsdepartments : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Philantropists", "DonationID", "dbo.Departments");
            CreateTable(
                "dbo.PhilantropistDepartments",
                c => new
                    {
                        Philantropist_PhilantropistID = c.Int(nullable: false),
                        Department_department_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Philantropist_PhilantropistID, t.Department_department_id })
                .ForeignKey("dbo.Philantropists", t => t.Philantropist_PhilantropistID, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.Department_department_id, cascadeDelete: true)
                .Index(t => t.Philantropist_PhilantropistID)
                .Index(t => t.Department_department_id);
            
            AddForeignKey("dbo.Philantropists", "DonationID", "dbo.Donations", "DonationID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Philantropists", "DonationID", "dbo.Donations");
            DropForeignKey("dbo.PhilantropistDepartments", "Department_department_id", "dbo.Departments");
            DropForeignKey("dbo.PhilantropistDepartments", "Philantropist_PhilantropistID", "dbo.Philantropists");
            DropIndex("dbo.PhilantropistDepartments", new[] { "Department_department_id" });
            DropIndex("dbo.PhilantropistDepartments", new[] { "Philantropist_PhilantropistID" });
            DropTable("dbo.PhilantropistDepartments");
            AddForeignKey("dbo.Philantropists", "DonationID", "dbo.Departments", "department_id", cascadeDelete: true);
        }
    }
}
