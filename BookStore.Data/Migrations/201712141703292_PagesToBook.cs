namespace BookStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PagesToBook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Pages", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Pages");
        }
    }
}
