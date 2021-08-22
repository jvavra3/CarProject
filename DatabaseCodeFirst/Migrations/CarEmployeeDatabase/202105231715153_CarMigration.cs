namespace CarProject.DatabaseCodeFirst.Migrations.CarEmployeeDatabase
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CarMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeModels",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        PhoneNumber = c.Int(nullable: false),
                        FactoryName = c.String(),
                        Factory_FactoryId = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.FactoryModels", t => t.Factory_FactoryId)
                .Index(t => t.Factory_FactoryId);
            
            CreateTable(
                "dbo.FactoryModels",
                c => new
                    {
                        FactoryId = c.Int(nullable: false, identity: true),
                        FactoryName = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        PhoneNumber = c.Int(nullable: false),
                        Email = c.String(nullable: false),
                        TypeOfCar = c.String(),
                    })
                .PrimaryKey(t => t.FactoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeModels", "Factory_FactoryId", "dbo.FactoryModels");
            DropIndex("dbo.EmployeeModels", new[] { "Factory_FactoryId" });
            DropTable("dbo.FactoryModels");
            DropTable("dbo.EmployeeModels");
        }
    }
}
