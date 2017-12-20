namespace BookStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BooksChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Price", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Books", "Pages", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Pages", c => c.Int(nullable: false));
            AlterColumn("dbo.Books", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
