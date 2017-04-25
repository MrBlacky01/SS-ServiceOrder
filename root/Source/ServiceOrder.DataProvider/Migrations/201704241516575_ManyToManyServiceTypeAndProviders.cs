namespace ServiceOrder.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManyToManyServiceTypeAndProviders : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ServiceTypes", "ServiceProvider_UserId", "dbo.ServiceProviders");
            DropIndex("dbo.ServiceTypes", new[] { "ServiceProvider_UserId" });
            CreateTable(
                "dbo.ServiceTypeServiceProviders",
                c => new
                    {
                        ServiceType_Id = c.Int(nullable: false),
                        ServiceProvider_UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ServiceType_Id, t.ServiceProvider_UserId })
                .ForeignKey("dbo.ServiceTypes", t => t.ServiceType_Id, cascadeDelete: true)
                .ForeignKey("dbo.ServiceProviders", t => t.ServiceProvider_UserId, cascadeDelete: true)
                .Index(t => t.ServiceType_Id)
                .Index(t => t.ServiceProvider_UserId);
            
            DropColumn("dbo.ServiceTypes", "ServiceProvider_UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ServiceTypes", "ServiceProvider_UserId", c => c.String(maxLength: 128));
            DropForeignKey("dbo.ServiceTypeServiceProviders", "ServiceProvider_UserId", "dbo.ServiceProviders");
            DropForeignKey("dbo.ServiceTypeServiceProviders", "ServiceType_Id", "dbo.ServiceTypes");
            DropIndex("dbo.ServiceTypeServiceProviders", new[] { "ServiceProvider_UserId" });
            DropIndex("dbo.ServiceTypeServiceProviders", new[] { "ServiceType_Id" });
            DropTable("dbo.ServiceTypeServiceProviders");
            CreateIndex("dbo.ServiceTypes", "ServiceProvider_UserId");
            AddForeignKey("dbo.ServiceTypes", "ServiceProvider_UserId", "dbo.ServiceProviders", "UserId");
        }
    }
}
