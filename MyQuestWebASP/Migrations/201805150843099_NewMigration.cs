namespace MyQuestWebASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderModels",
                c => new
                    {
                        OrderModelId = c.Int(nullable: false, identity: true),
                        NameClient = c.String(),
                        PhoneNumbClient = c.String(),
                        ClientPayForOrder = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderCreateDateTime = c.DateTime(nullable: false),
                        StatusId = c.Int(nullable: false),
                        OrderLastModifDateTime = c.DateTime(nullable: false),
                        ServiceId = c.Int(nullable: false),
                        QuantityItem = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderModelId)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.StatusId)
                .Index(t => t.ServiceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderModels", "StatusId", "dbo.Status");
            DropForeignKey("dbo.OrderModels", "ServiceId", "dbo.Services");
            DropIndex("dbo.OrderModels", new[] { "ServiceId" });
            DropIndex("dbo.OrderModels", new[] { "StatusId" });
            DropTable("dbo.OrderModels");
        }
    }
}
