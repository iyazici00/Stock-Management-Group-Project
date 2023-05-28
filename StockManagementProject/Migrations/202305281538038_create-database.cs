namespace StockManagementProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Name = c.String(),
                        IsStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Shipments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReceiverWarehouseId = c.Int(nullable: false),
                        ShipperWarehouseId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        ProductQuantity = c.Int(nullable: false),
                        ShipmentDate = c.DateTime(nullable: false),
                        ShipperManagerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.ShipperManagerId, cascadeDelete: true)
                .ForeignKey("dbo.Warehouses", t => t.ReceiverWarehouseId)
                .ForeignKey("dbo.Warehouses", t => t.ShipperWarehouseId)
                .Index(t => t.ReceiverWarehouseId)
                .Index(t => t.ShipperWarehouseId)
                .Index(t => t.ProductId)
                .Index(t => t.ShipperManagerId);
            
            CreateTable(
                "dbo.Warehouses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        District = c.String(),
                        ManagerId = c.Int(nullable: false),
                        Name = c.String(),
                        IsStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ManagerId, cascadeDelete: true)
                .Index(t => t.ManagerId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Surname = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        RoleId = c.Int(nullable: false),
                        Name = c.String(),
                        IsStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WarehouseProductStocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WarehouseId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        ProductQuantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Warehouses", t => t.WarehouseId, cascadeDelete: true)
                .Index(t => t.WarehouseId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shipments", "ShipperWarehouseId", "dbo.Warehouses");
            DropForeignKey("dbo.Shipments", "ReceiverWarehouseId", "dbo.Warehouses");
            DropForeignKey("dbo.WarehouseProductStocks", "WarehouseId", "dbo.Warehouses");
            DropForeignKey("dbo.WarehouseProductStocks", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Warehouses", "ManagerId", "dbo.Users");
            DropForeignKey("dbo.Shipments", "ShipperManagerId", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Shipments", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.WarehouseProductStocks", new[] { "ProductId" });
            DropIndex("dbo.WarehouseProductStocks", new[] { "WarehouseId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Warehouses", new[] { "ManagerId" });
            DropIndex("dbo.Shipments", new[] { "ShipperManagerId" });
            DropIndex("dbo.Shipments", new[] { "ProductId" });
            DropIndex("dbo.Shipments", new[] { "ShipperWarehouseId" });
            DropIndex("dbo.Shipments", new[] { "ReceiverWarehouseId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropTable("dbo.WarehouseProductStocks");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Warehouses");
            DropTable("dbo.Shipments");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
