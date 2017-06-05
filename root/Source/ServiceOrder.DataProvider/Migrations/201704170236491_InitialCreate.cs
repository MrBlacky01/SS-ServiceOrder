namespace ServiceOrder.DataProvider.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PhotoId = c.Int(),
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
                .ForeignKey("dbo.Photos", t => t.PhotoId)
                .Index(t => t.PhotoId)
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
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhotoImage = c.Binary(nullable: false, storeType: "image"),
                        ServiceProvider_UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServiceProviders", t => t.ServiceProvider_UserId)
                .Index(t => t.ServiceProvider_UserId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.String(),
                        RegionId = c.Int(nullable: false),
                        ServiceTypeId = c.Int(nullable: false),
                        ServiceProviderId = c.Int(nullable: false),
                        BeginTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        OrderClient_UserId = c.String(maxLength: 128),
                        OrderProvider_UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.OrderClient_UserId)
                .ForeignKey("dbo.ServiceProviders", t => t.OrderProvider_UserId)
                .ForeignKey("dbo.Regions", t => t.RegionId, cascadeDelete: true)
                .ForeignKey("dbo.ServiceTypes", t => t.ServiceTypeId, cascadeDelete: true)
                .Index(t => t.RegionId)
                .Index(t => t.ServiceTypeId)
                .Index(t => t.OrderClient_UserId)
                .Index(t => t.OrderProvider_UserId);
            
            CreateTable(
                "dbo.ServiceProviders",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        WorkingTime = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiceTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        ServiceCategoryId = c.Int(nullable: false),
                        ServiceProvider_UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServiceCategories", t => t.ServiceCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.ServiceProviders", t => t.ServiceProvider_UserId)
                .Index(t => t.ServiceCategoryId)
                .Index(t => t.ServiceProvider_UserId);
            
            CreateTable(
                "dbo.ServiceCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.RegionServiceProviders",
                c => new
                    {
                        Region_Id = c.Int(nullable: false),
                        ServiceProvider_UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Region_Id, t.ServiceProvider_UserId })
                .ForeignKey("dbo.Regions", t => t.Region_Id, cascadeDelete: true)
                .ForeignKey("dbo.ServiceProviders", t => t.ServiceProvider_UserId, cascadeDelete: true)
                .Index(t => t.Region_Id)
                .Index(t => t.ServiceProvider_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Orders", "ServiceTypeId", "dbo.ServiceTypes");
            DropForeignKey("dbo.Orders", "RegionId", "dbo.Regions");
            DropForeignKey("dbo.Orders", "OrderProvider_UserId", "dbo.ServiceProviders");
            DropForeignKey("dbo.ServiceProviders", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ServiceTypes", "ServiceProvider_UserId", "dbo.ServiceProviders");
            DropForeignKey("dbo.ServiceTypes", "ServiceCategoryId", "dbo.ServiceCategories");
            DropForeignKey("dbo.RegionServiceProviders", "ServiceProvider_UserId", "dbo.ServiceProviders");
            DropForeignKey("dbo.RegionServiceProviders", "Region_Id", "dbo.Regions");
            DropForeignKey("dbo.Photos", "ServiceProvider_UserId", "dbo.ServiceProviders");
            DropForeignKey("dbo.Orders", "OrderClient_UserId", "dbo.Clients");
            DropForeignKey("dbo.Clients", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "PhotoId", "dbo.Photos");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.RegionServiceProviders", new[] { "ServiceProvider_UserId" });
            DropIndex("dbo.RegionServiceProviders", new[] { "Region_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ServiceTypes", new[] { "ServiceProvider_UserId" });
            DropIndex("dbo.ServiceTypes", new[] { "ServiceCategoryId" });
            DropIndex("dbo.ServiceProviders", new[] { "UserId" });
            DropIndex("dbo.Orders", new[] { "OrderProvider_UserId" });
            DropIndex("dbo.Orders", new[] { "OrderClient_UserId" });
            DropIndex("dbo.Orders", new[] { "ServiceTypeId" });
            DropIndex("dbo.Orders", new[] { "RegionId" });
            DropIndex("dbo.Photos", new[] { "ServiceProvider_UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "PhotoId" });
            DropIndex("dbo.Clients", new[] { "UserId" });
            DropTable("dbo.RegionServiceProviders");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ServiceCategories");
            DropTable("dbo.ServiceTypes");
            DropTable("dbo.Regions");
            DropTable("dbo.ServiceProviders");
            DropTable("dbo.Orders");
            DropTable("dbo.Photos");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Clients");
        }
    }
}
