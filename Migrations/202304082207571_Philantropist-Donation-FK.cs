namespace hospital_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhilantropistDonationFK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Philantropists", "DonationID", c => c.Int(nullable: false));
            CreateIndex("dbo.Philantropists", "DonationID");
            AddForeignKey("dbo.Philantropists", "DonationID", "dbo.Departments", "department_id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Philantropists", "DonationID", "dbo.Departments");
            DropIndex("dbo.Philantropists", new[] { "DonationID" });
            DropColumn("dbo.Philantropists", "DonationID");
        }
    }
}
