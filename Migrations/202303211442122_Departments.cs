namespace hospital_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Departments : DbMigration
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
            
            //DropTable("dbo.Doctors");
            //DropTable("dbo.Tests");
        }
        
        public override void Down()
        {
            /**CreateTable(
                "dbo.Tests",
                c => new
                    {
                        doctor_id = c.Int(nullable: false, identity: true),
                        doctor_name = c.String(),
                    })
                .PrimaryKey(t => t.doctor_id);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        doctor_id = c.Int(nullable: false, identity: true),
                        first_name = c.String(),
                        last_name = c.String(),
                        email = c.String(),
                    })
                .PrimaryKey(t => t.doctor_id); **/

            DropTable("dbo.Departments");
        }
    }
}
