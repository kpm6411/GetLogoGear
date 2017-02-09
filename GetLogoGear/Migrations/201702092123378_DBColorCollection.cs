namespace GetLogoGear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBColorCollection : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ColorBaseItems", newName: "T_BaseItem_Color");
            RenameColumn(table: "dbo.T_BaseItem_Color", name: "Color_ColorID", newName: "ColorID");
            RenameColumn(table: "dbo.T_BaseItem_Color", name: "BaseItem_ItemID", newName: "BaseItemID");
            RenameIndex(table: "dbo.T_BaseItem_Color", name: "IX_BaseItem_ItemID", newName: "IX_BaseItemID");
            RenameIndex(table: "dbo.T_BaseItem_Color", name: "IX_Color_ColorID", newName: "IX_ColorID");
            DropPrimaryKey("dbo.T_BaseItem_Color");
            AddPrimaryKey("dbo.T_BaseItem_Color", new[] { "BaseItemID", "ColorID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.T_BaseItem_Color");
            AddPrimaryKey("dbo.T_BaseItem_Color", new[] { "Color_ColorID", "BaseItem_ItemID" });
            RenameIndex(table: "dbo.T_BaseItem_Color", name: "IX_ColorID", newName: "IX_Color_ColorID");
            RenameIndex(table: "dbo.T_BaseItem_Color", name: "IX_BaseItemID", newName: "IX_BaseItem_ItemID");
            RenameColumn(table: "dbo.T_BaseItem_Color", name: "BaseItemID", newName: "BaseItem_ItemID");
            RenameColumn(table: "dbo.T_BaseItem_Color", name: "ColorID", newName: "Color_ColorID");
            RenameTable(name: "dbo.T_BaseItem_Color", newName: "ColorBaseItems");
        }
    }
}
