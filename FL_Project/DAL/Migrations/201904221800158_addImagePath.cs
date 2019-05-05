namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addImagePath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Drops", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Drops", "ImagePath");
        }
    }
}
