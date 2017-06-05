namespace ServiceOrder.DataProvider.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class DeleteInServiceType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ServiceTypes", "ServiceCategoryId", "dbo.ServiceCategories");
            DropIndex("dbo.ServiceTypes", new[] { "ServiceCategoryId" });
            RenameColumn(table: "dbo.ServiceTypes", name: "ServiceCategoryId", newName: "Category_Id");
            AlterColumn("dbo.ServiceTypes", "Category_Id", c => c.Int());
            CreateIndex("dbo.ServiceTypes", "Category_Id");
            AddForeignKey("dbo.ServiceTypes", "Category_Id", "dbo.ServiceCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceTypes", "Category_Id", "dbo.ServiceCategories");
            DropIndex("dbo.ServiceTypes", new[] { "Category_Id" });
            AlterColumn("dbo.ServiceTypes", "Category_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.ServiceTypes", name: "Category_Id", newName: "ServiceCategoryId");
            CreateIndex("dbo.ServiceTypes", "ServiceCategoryId");
            AddForeignKey("dbo.ServiceTypes", "ServiceCategoryId", "dbo.ServiceCategories", "Id", cascadeDelete: true);
        }
    }
}
