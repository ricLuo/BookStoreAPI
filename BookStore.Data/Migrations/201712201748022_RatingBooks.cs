namespace BookStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RatingBooks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Rating", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Rating");
        }
    }
}
