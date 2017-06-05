namespace ServiceOrder.DataProvider.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ChangeIDtoStringMigrations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "ServiceProviderId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "ServiceProviderId", c => c.Int(nullable: false));
        }
    }
}
