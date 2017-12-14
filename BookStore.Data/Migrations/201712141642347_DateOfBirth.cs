namespace BookStore.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class DateOfBirth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "DateOfBirth", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "DateOfBirth");
        }
    }
}
