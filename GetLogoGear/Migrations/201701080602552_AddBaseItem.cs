namespace GetLogoGear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBaseItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BaseItems",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Sizes = c.Boolean(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.ItemID);
            
            AddColumn("dbo.Colors", "BaseItem_ItemID", c => c.Int());
            CreateIndex("dbo.Colors", "BaseItem_ItemID");
            AddForeignKey("dbo.Colors", "BaseItem_ItemID", "dbo.BaseItems", "ItemID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Colors", "BaseItem_ItemID", "dbo.BaseItems");
            DropIndex("dbo.Colors", new[] { "BaseItem_ItemID" });
            DropColumn("dbo.Colors", "BaseItem_ItemID");
            DropTable("dbo.BaseItems");
        }
    }
}
