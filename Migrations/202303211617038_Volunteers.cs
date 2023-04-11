namespace hospital_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Volunteers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Volunteers",
                c => new
                    {
                        volunteer_id = c.Int(nullable: false, identity: true),
                        first_name = c.String(),
                        last_name = c.String(),
                        email = c.String(),
                    })
                .PrimaryKey(t => t.volunteer_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Volunteers");
        }
    }
}
