namespace ServiceOrder.DataProvider.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAlbum : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Photos", "ServiceProvider_UserId", "dbo.ServiceProviders");
            DropIndex("dbo.Photos", new[] { "ServiceProvider_UserId" });
            RenameColumn(table: "dbo.AspNetUsers", name: "PhotoId", newName: "UserPhoto_Id");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_PhotoId", newName: "IX_UserPhoto_Id");
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Provider_UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServiceProviders", t => t.Provider_UserId)
                .Index(t => t.Provider_UserId);
            
            AddColumn("dbo.Photos", "FileName", c => c.String(maxLength: 255));
            AddColumn("dbo.Photos", "ContentType", c => c.String(maxLength: 100));
            AddColumn("dbo.Photos", "PhotoAlbum_Id", c => c.Int());
            CreateIndex("dbo.Photos", "PhotoAlbum_Id");
            AddForeignKey("dbo.Photos", "PhotoAlbum_Id", "dbo.Albums", "Id");
            DropColumn("dbo.Photos", "ServiceProvider_UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Photos", "ServiceProvider_UserId", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Albums", "Provider_UserId", "dbo.ServiceProviders");
            DropForeignKey("dbo.Photos", "PhotoAlbum_Id", "dbo.Albums");
            DropIndex("dbo.Photos", new[] { "PhotoAlbum_Id" });
            DropIndex("dbo.Albums", new[] { "Provider_UserId" });
            DropColumn("dbo.Photos", "PhotoAlbum_Id");
            DropColumn("dbo.Photos", "ContentType");
            DropColumn("dbo.Photos", "FileName");
            DropTable("dbo.Albums");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_UserPhoto_Id", newName: "IX_PhotoId");
            RenameColumn(table: "dbo.AspNetUsers", name: "UserPhoto_Id", newName: "PhotoId");
            CreateIndex("dbo.Photos", "ServiceProvider_UserId");
            AddForeignKey("dbo.Photos", "ServiceProvider_UserId", "dbo.ServiceProviders", "UserId");
        }
    }
}
