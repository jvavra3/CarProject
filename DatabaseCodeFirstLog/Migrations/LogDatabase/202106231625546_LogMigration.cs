namespace CarProject.DatabaseCodeFirstLog.Migrations.LogDatabase
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LogMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogModels",
                c => new
                    {
                        LogId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        IpAddress = c.String(),
                        Area = c.String(),
                        TimeAccess = c.String(),
                    })
                .PrimaryKey(t => t.LogId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LogModels");
        }
    }
}
