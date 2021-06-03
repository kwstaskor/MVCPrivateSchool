namespace MVCSchool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingStringLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Students", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Trainers", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Trainers", "LastName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trainers", "LastName", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Trainers", "FirstName", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Students", "LastName", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Students", "FirstName", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
