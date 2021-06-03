namespace MVCSchool.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class initialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        AssignmentId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 50),
                        Submission = c.DateTime(nullable: false),
                        OralMark = c.Single(nullable: false),
                        TotalMark = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.AssignmentId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Stream = c.String(nullable: false, maxLength: 50),
                        Type = c.String(nullable: false, maxLength: 50),
                        StartDate = c.DateTime(nullable: false, storeType: "date"),
                        EndDate = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 20),
                        DateOfBirth = c.DateTime(nullable: false, storeType: "date"),
                        TuitionFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.StudentId);
            
            CreateTable(
                "dbo.Trainers",
                c => new
                    {
                        TrainerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 20),
                        Subject = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.TrainerId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AssignmentsCourses",
                c => new
                    {
                        CourseId = c.Int(nullable: false),
                        AssignmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CourseId, t.AssignmentId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Assignments", t => t.AssignmentId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.AssignmentId);
            
            CreateTable(
                "dbo.StudentsAssignments",
                c => new
                    {
                        StudentId = c.Int(nullable: false),
                        AssignmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentId, t.AssignmentId })
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .ForeignKey("dbo.Assignments", t => t.AssignmentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.AssignmentId);
            
            CreateTable(
                "dbo.StudentsCourses",
                c => new
                    {
                        CourseId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CourseId, t.StudentId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.TrainersCourses",
                c => new
                    {
                        CourseId = c.Int(nullable: false),
                        TrainerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CourseId, t.TrainerId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Trainers", t => t.TrainerId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.TrainerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.TrainersCourses", "TrainerId", "dbo.Trainers");
            DropForeignKey("dbo.TrainersCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.StudentsCourses", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentsCourses", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.StudentsAssignments", "AssignmentId", "dbo.Assignments");
            DropForeignKey("dbo.StudentsAssignments", "StudentId", "dbo.Students");
            DropForeignKey("dbo.AssignmentsCourses", "AssignmentId", "dbo.Assignments");
            DropForeignKey("dbo.AssignmentsCourses", "CourseId", "dbo.Courses");
            DropIndex("dbo.TrainersCourses", new[] { "TrainerId" });
            DropIndex("dbo.TrainersCourses", new[] { "CourseId" });
            DropIndex("dbo.StudentsCourses", new[] { "StudentId" });
            DropIndex("dbo.StudentsCourses", new[] { "CourseId" });
            DropIndex("dbo.StudentsAssignments", new[] { "AssignmentId" });
            DropIndex("dbo.StudentsAssignments", new[] { "StudentId" });
            DropIndex("dbo.AssignmentsCourses", new[] { "AssignmentId" });
            DropIndex("dbo.AssignmentsCourses", new[] { "CourseId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.TrainersCourses");
            DropTable("dbo.StudentsCourses");
            DropTable("dbo.StudentsAssignments");
            DropTable("dbo.AssignmentsCourses");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Trainers");
            DropTable("dbo.Students");
            DropTable("dbo.Courses");
            DropTable("dbo.Assignments");
        }
    }
}
