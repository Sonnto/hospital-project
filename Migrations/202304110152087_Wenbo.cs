namespace hospital_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Wenbo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PhysicianDepartments", "Physician_physician_id", "dbo.Physicians");
            DropForeignKey("dbo.PhysicianDepartments", "Department_department_id", "dbo.Departments");
            DropForeignKey("dbo.Availabilities", "department_id", "dbo.Departments");
            DropForeignKey("dbo.Availabilities", "physician_id", "dbo.Physicians");
            DropIndex("dbo.Availabilities", new[] { "physician_id" });
            DropIndex("dbo.Availabilities", new[] { "department_id" });
            DropIndex("dbo.PhysicianDepartments", new[] { "Physician_physician_id" });
            DropIndex("dbo.PhysicianDepartments", new[] { "Department_department_id" });
            DropTable("dbo.Availabilities");
            DropTable("dbo.PhysicianDepartments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PhysicianDepartments",
                c => new
                    {
                        Physician_physician_id = c.Int(nullable: false),
                        Department_department_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Physician_physician_id, t.Department_department_id });
            
            CreateTable(
                "dbo.Availabilities",
                c => new
                    {
                        availability_id = c.Int(nullable: false, identity: true),
                        physician_id = c.Int(nullable: false),
                        department_id = c.Int(nullable: false),
                        availability_dates = c.String(),
                    })
                .PrimaryKey(t => t.availability_id);
            
            CreateIndex("dbo.PhysicianDepartments", "Department_department_id");
            CreateIndex("dbo.PhysicianDepartments", "Physician_physician_id");
            CreateIndex("dbo.Availabilities", "department_id");
            CreateIndex("dbo.Availabilities", "physician_id");
            AddForeignKey("dbo.Availabilities", "physician_id", "dbo.Physicians", "physician_id", cascadeDelete: true);
            AddForeignKey("dbo.Availabilities", "department_id", "dbo.Departments", "department_id", cascadeDelete: true);
            AddForeignKey("dbo.PhysicianDepartments", "Department_department_id", "dbo.Departments", "department_id", cascadeDelete: true);
            AddForeignKey("dbo.PhysicianDepartments", "Physician_physician_id", "dbo.Physicians", "physician_id", cascadeDelete: true);
        }
    }
}
