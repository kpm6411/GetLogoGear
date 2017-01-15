namespace GetLogoGear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImagePath : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BaseItems", "Image", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BaseItems", "Image", c => c.Binary());
        }
    }
}
