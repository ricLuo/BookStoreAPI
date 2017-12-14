namespace BookStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PublishingCompanyBookTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "PublishingCompany", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "PublishingCompany");
        }
    }
}
