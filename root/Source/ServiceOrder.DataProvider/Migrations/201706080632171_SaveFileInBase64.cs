namespace ServiceOrder.DataProvider.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class SaveFileInBase64 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Photos", "PhotoImage");
            AddColumn("dbo.Photos", "PhotoImage", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Photos", "PhotoImage", c => c.Binary(nullable: false, storeType: "image"));
        }
    }
}
