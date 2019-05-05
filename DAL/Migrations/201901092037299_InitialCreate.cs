namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Drops",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Drop_Id = c.Int(nullable: false),
                        Drop_Adress = c.String(),
                        Drop_time = c.DateTime(nullable: false),
                        Real_lat = c.Double(nullable: false),
                        Real_log = c.Double(nullable: false),
                        Estimeated_lat = c.Double(nullable: false),
                        Estimeated_log = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Report_Id = c.Int(nullable: false),
                        Time = c.DateTime(nullable: false),
                        Name = c.String(),
                        Report_Adress = c.String(),
                        Boom_count = c.Int(nullable: false),
                        ImagePath = c.String(),
                        lat = c.Double(nullable: false),
                        log = c.Double(nullable: false),
                        Drop_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Drops", t => t.Drop_Id)
                .Index(t => t.Drop_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reports", "Drop_Id", "dbo.Drops");
            DropIndex("dbo.Reports", new[] { "Drop_Id" });
            DropTable("dbo.Reports");
            DropTable("dbo.Drops");
        }
    }
}
