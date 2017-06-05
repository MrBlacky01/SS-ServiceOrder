namespace ServiceOrder.DataProvider.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class rebaseOrder : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "RegionId", "dbo.Regions");
            DropForeignKey("dbo.Orders", "ServiceTypeId", "dbo.ServiceTypes");
            DropIndex("dbo.Orders", new[] { "RegionId" });
            DropIndex("dbo.Orders", new[] { "ServiceTypeId" });
            RenameColumn(table: "dbo.Orders", name: "RegionId", newName: "OrderRegion_Id");
            RenameColumn(table: "dbo.Orders", name: "ServiceTypeId", newName: "OrderType_Id");
            AlterColumn("dbo.Orders", "OrderRegion_Id", c => c.Int());
            AlterColumn("dbo.Orders", "OrderType_Id", c => c.Int());
            CreateIndex("dbo.Orders", "OrderRegion_Id");
            CreateIndex("dbo.Orders", "OrderType_Id");
            AddForeignKey("dbo.Orders", "OrderRegion_Id", "dbo.Regions", "Id");
            AddForeignKey("dbo.Orders", "OrderType_Id", "dbo.ServiceTypes", "Id");
            DropColumn("dbo.Orders", "ClientId");
            DropColumn("dbo.Orders", "ServiceProviderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "ServiceProviderId", c => c.String());
            AddColumn("dbo.Orders", "ClientId", c => c.String());
            DropForeignKey("dbo.Orders", "OrderType_Id", "dbo.ServiceTypes");
            DropForeignKey("dbo.Orders", "OrderRegion_Id", "dbo.Regions");
            DropIndex("dbo.Orders", new[] { "OrderType_Id" });
            DropIndex("dbo.Orders", new[] { "OrderRegion_Id" });
            AlterColumn("dbo.Orders", "OrderType_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "OrderRegion_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Orders", name: "OrderType_Id", newName: "ServiceTypeId");
            RenameColumn(table: "dbo.Orders", name: "OrderRegion_Id", newName: "RegionId");
            CreateIndex("dbo.Orders", "ServiceTypeId");
            CreateIndex("dbo.Orders", "RegionId");
            AddForeignKey("dbo.Orders", "ServiceTypeId", "dbo.ServiceTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "RegionId", "dbo.Regions", "Id", cascadeDelete: true);
        }
    }
}
