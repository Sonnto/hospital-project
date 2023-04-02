namespace hospital_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhysiciansDepartments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PhysicianDepartments",
                c => new
                    {
                        Physician_physician_id = c.Int(nullable: false),
                        Department_department_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Physician_physician_id, t.Department_department_id })
                .ForeignKey("dbo.Physicians", t => t.Physician_physician_id, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.Department_department_id, cascadeDelete: true)
                .Index(t => t.Physician_physician_id)
                .Index(t => t.Department_department_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhysicianDepartments", "Department_department_id", "dbo.Departments");
            DropForeignKey("dbo.PhysicianDepartments", "Physician_physician_id", "dbo.Physicians");
            DropIndex("dbo.PhysicianDepartments", new[] { "Department_department_id" });
            DropIndex("dbo.PhysicianDepartments", new[] { "Physician_physician_id" });
            DropTable("dbo.PhysicianDepartments");
        }
    }
}
