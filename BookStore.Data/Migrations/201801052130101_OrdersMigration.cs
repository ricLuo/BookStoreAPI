namespace BookStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdersMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderGuid = c.Guid(nullable: false),
                        CustomerId = c.String(maxLength: 128),
                        OrderStatusId = c.Int(nullable: false),
                        OrderTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderCreatedDate = c.DateTime(nullable: false),
                        OrderStatus = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.ShoppingCarts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.String(maxLength: 128),
                        BookId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.CustomerId)
                .Index(t => t.CustomerId)
                .Index(t => t.BookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingCarts", "CustomerId", "dbo.Users");
            DropForeignKey("dbo.ShoppingCarts", "BookId", "dbo.Books");
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Users");
            DropForeignKey("dbo.OrderItems", "BookId", "dbo.Books");
            DropIndex("dbo.ShoppingCarts", new[] { "BookId" });
            DropIndex("dbo.ShoppingCarts", new[] { "CustomerId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.OrderItems", new[] { "BookId" });
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropTable("dbo.ShoppingCarts");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
        }
    }
}
