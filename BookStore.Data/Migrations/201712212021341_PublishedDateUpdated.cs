namespace BookStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PublishedDateUpdated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "PublishedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "PublishedDate", c => c.DateTime(nullable: false));
        }
    }
}
