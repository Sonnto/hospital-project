namespace hospital_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class philantropists : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Philantropists",
                c => new
                    {
                        PhilantropistID = c.Int(nullable: false, identity: true),
                        PhilantropistFirstName = c.String(),
                        PhilantropistLastName = c.String(),
                        Company = c.String(),
                    })
                .PrimaryKey(t => t.PhilantropistID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Volunteers", "department_id", "dbo.Departments");
            DropIndex("dbo.Volunteers", new[] { "department_id" });
            DropColumn("dbo.Volunteers", "department_id");
            DropTable("dbo.Philantropists");
        }
    }
}
