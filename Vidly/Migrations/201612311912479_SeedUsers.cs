namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'0c9a397d-4470-414a-9ce6-c46ee66f0a1e', N'admin@vidly.com', 0, N'AG+mKqRn7uEATw4OOOzYa8xiHLWV2G4bvURFYYWKQLR5PwJkmrk5fWOq01B2w8F2XQ==', N'4cff7bd4-402f-4cd6-b79d-19ba5fe1dcb7', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'e83756e6-efe0-4344-bfb8-1583fe968bbf', N'guest@vidly.com', 0, N'AORgNwDGcIlgyjopUtWDZUaz6KdyEL7RIxXNUdjQwiN7ggU+2lDrYyBDbnjAuJnqow==', N'fc6ff2ee-6988-4120-9d4f-3900a10400b5', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'81d6463f-36bf-4758-848e-73921794ff32', N'CanManageMovies')
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0c9a397d-4470-414a-9ce6-c46ee66f0a1e', N'81d6463f-36bf-4758-848e-73921794ff32')            
            ");
        }
        
        public override void Down()
        {
        }
    }
}
