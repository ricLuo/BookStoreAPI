namespace BookStore.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddAuthorUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Authors", "WebSite", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Authors", "WebSite");
        }
    }
}
