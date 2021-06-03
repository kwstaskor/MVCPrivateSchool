namespace MVCSchool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
              INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'a311096d-3321-4e92-8482-83cc8904ab1b', N'guest@codeacademy.com', 0, N'ABRWAvqnzBWCOQj3lHsrUDRenx8TKumeGmhmR/ckNZwrwRKVI6QarBC2uVHXR7EneQ==', N'dca66549-b71c-465e-ae7d-755fc7212d26', NULL, 0, 0, NULL, 1, 0, N'guest@codeacademy.com')
             INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'f7f8669d-c406-430f-a433-ed34d4255aa6', N'admin@codeacademy.com', 0, N'AF5exClejLelS4a1/o/oZKYtGUqfwa3h+kmvdfqMnrNgYNaxZ6WzxCcWovg0SH+cXw==', N'3326ec40-964a-4b44-b15f-aeb1f9cf6488', NULL, 0, 0, NULL, 1, 0, N'admin@codeacademy.com')
              INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'151467e6-49a4-4bee-9829-05ba8a6d878b', N'Admin')
              INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f7f8669d-c406-430f-a433-ed34d4255aa6', N'151467e6-49a4-4bee-9829-05ba8a6d878b')
                ");
        }

        public override void Down()
        {
        }
    }
}
