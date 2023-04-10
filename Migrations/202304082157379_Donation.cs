namespace hospital_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Donation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Volunteers", "department_id", "dbo.Departments");
            DropIndex("dbo.Volunteers", new[] { "department_id" });
            CreateTable(
                "dbo.Donations",
                c => new
                    {
                        DonationID = c.Int(nullable: false, identity: true),
                        DonationLevel = c.String(),
                        MinAmount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DonationID);
            
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.Volunteers", "department_id", c => c.Int(nullable: false));
            DropTable("dbo.Donations");
            CreateIndex("dbo.Volunteers", "department_id");
            AddForeignKey("dbo.Volunteers", "department_id", "dbo.Departments", "department_id", cascadeDelete: true);
        }
    }
}
