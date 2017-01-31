namespace GetLogoGear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateColorModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BaseItems", "HasSizes", c => c.Boolean(nullable: false));
            AlterColumn("dbo.BaseItems", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.BaseItems", "Description", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.BaseItems", "Price", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.Colors", "Name", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.BaseItems", "Sizes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BaseItems", "Sizes", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Colors", "Name", c => c.String());
            AlterColumn("dbo.BaseItems", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BaseItems", "Description", c => c.String());
            AlterColumn("dbo.BaseItems", "Name", c => c.String());
            DropColumn("dbo.BaseItems", "HasSizes");
        }
    }
}
