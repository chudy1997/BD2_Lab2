namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDb2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderProducts", "Order_OrderId", "dbo.Orders");
            DropIndex("dbo.OrderProducts", new[] { "Order_OrderId" });
            RenameColumn(table: "dbo.OrderProducts", name: "Order_OrderId", newName: "OrderId");
            AlterColumn("dbo.OrderProducts", "OrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderProducts", "OrderId");
            AddForeignKey("dbo.OrderProducts", "OrderId", "dbo.Orders", "OrderId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderProducts", "OrderId", "dbo.Orders");
            DropIndex("dbo.OrderProducts", new[] { "OrderId" });
            AlterColumn("dbo.OrderProducts", "OrderId", c => c.Int());
            RenameColumn(table: "dbo.OrderProducts", name: "OrderId", newName: "Order_OrderId");
            CreateIndex("dbo.OrderProducts", "Order_OrderId");
            AddForeignKey("dbo.OrderProducts", "Order_OrderId", "dbo.Orders", "OrderId");
        }
    }
}
