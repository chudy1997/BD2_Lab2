namespace Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderProducts",
                c => new
                    {
                        OrderProductId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Cost = c.Decimal(nullable: false, storeType: "money"),
                        Order_OrderId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderProductId)
                .ForeignKey("dbo.Orders", t => t.Order_OrderId)
                .Index(t => t.Order_OrderId);
            
            AddColumn("dbo.Orders", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Orders", "ProductId");
            DropColumn("dbo.Orders", "Quantity");
            DropColumn("dbo.Orders", "Cost");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Cost", c => c.Decimal(nullable: false, storeType: "money"));
            AddColumn("dbo.Orders", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "ProductId", c => c.Int(nullable: false));
            DropForeignKey("dbo.OrderProducts", "Order_OrderId", "dbo.Orders");
            DropIndex("dbo.OrderProducts", new[] { "Order_OrderId" });
            DropColumn("dbo.Orders", "Date");
            DropTable("dbo.OrderProducts");
        }
    }
}
