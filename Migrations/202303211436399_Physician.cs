namespace hospital_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Physician : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Physicians",
                c => new
                    {
                        physician_id = c.Int(nullable: false, identity: true),
                        first_name = c.String(),
                        last_name = c.String(),
                        email = c.String(),
                    })
                .PrimaryKey(t => t.physician_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Physicians");
        }
    }
}
