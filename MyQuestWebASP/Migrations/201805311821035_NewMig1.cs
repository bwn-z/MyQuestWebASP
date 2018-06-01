namespace MyQuestWebASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMig1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderModels", "ServiceId", "dbo.Services");
            DropIndex("dbo.OrderModels", new[] { "ServiceId" });
            RenameColumn(table: "dbo.OrderModels", name: "ServiceId", newName: "Service_ServiceId");
            AddColumn("dbo.Services", "OrderModel_OrderModelId", c => c.Int());
            AlterColumn("dbo.OrderModels", "Service_ServiceId", c => c.Int());
            CreateIndex("dbo.OrderModels", "Service_ServiceId");
            CreateIndex("dbo.Services", "OrderModel_OrderModelId");
            AddForeignKey("dbo.Services", "OrderModel_OrderModelId", "dbo.OrderModels", "OrderModelId");
            AddForeignKey("dbo.OrderModels", "Service_ServiceId", "dbo.Services", "ServiceId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderModels", "Service_ServiceId", "dbo.Services");
            DropForeignKey("dbo.Services", "OrderModel_OrderModelId", "dbo.OrderModels");
            DropIndex("dbo.Services", new[] { "OrderModel_OrderModelId" });
            DropIndex("dbo.OrderModels", new[] { "Service_ServiceId" });
            AlterColumn("dbo.OrderModels", "Service_ServiceId", c => c.Int(nullable: false));
            DropColumn("dbo.Services", "OrderModel_OrderModelId");
            RenameColumn(table: "dbo.OrderModels", name: "Service_ServiceId", newName: "ServiceId");
            CreateIndex("dbo.OrderModels", "ServiceId");
            AddForeignKey("dbo.OrderModels", "ServiceId", "dbo.Services", "ServiceId", cascadeDelete: true);
        }
    }
}
