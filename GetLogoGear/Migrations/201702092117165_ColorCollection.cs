namespace GetLogoGear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColorCollection : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Colors", "BaseItem_ItemID", "dbo.BaseItems");
            DropIndex("dbo.Colors", new[] { "BaseItem_ItemID" });
            CreateTable(
                "dbo.ColorBaseItems",
                c => new
                    {
                        Color_ColorID = c.Int(nullable: false),
                        BaseItem_ItemID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Color_ColorID, t.BaseItem_ItemID })
                .ForeignKey("dbo.Colors", t => t.Color_ColorID, cascadeDelete: true)
                .ForeignKey("dbo.BaseItems", t => t.BaseItem_ItemID, cascadeDelete: true)
                .Index(t => t.Color_ColorID)
                .Index(t => t.BaseItem_ItemID);
            
            DropColumn("dbo.Colors", "BaseItem_ItemID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Colors", "BaseItem_ItemID", c => c.Int());
            DropForeignKey("dbo.ColorBaseItems", "BaseItem_ItemID", "dbo.BaseItems");
            DropForeignKey("dbo.ColorBaseItems", "Color_ColorID", "dbo.Colors");
            DropIndex("dbo.ColorBaseItems", new[] { "BaseItem_ItemID" });
            DropIndex("dbo.ColorBaseItems", new[] { "Color_ColorID" });
            DropTable("dbo.ColorBaseItems");
            CreateIndex("dbo.Colors", "BaseItem_ItemID");
            AddForeignKey("dbo.Colors", "BaseItem_ItemID", "dbo.BaseItems", "ItemID");
        }
    }
}
