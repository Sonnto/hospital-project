namespace hospital_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Patients : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        patient_id = c.Int(nullable: false, identity: true),
                        patient_fname = c.String(),
                        patient_surname = c.String(),
                        patient_condition = c.String(),
                        primary_physician_id = c.String(),
                    })
                .PrimaryKey(t => t.patient_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Patients");
        }
    }
}
