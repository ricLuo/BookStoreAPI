namespace BookStore.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<BookStore.Data.Common.BookStoreDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        //protected override void Seed(BookStoreDbContext context)
        //{
        //    //  This method will be called after migrating to the latest version.

        //    var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new BookStoreDbContext()));

        //    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new BookStoreDbContext()));

        //    var user = new ApplicationUser()
        //    {
        //        UserName = "SuperPowerUser",
        //        Email = "abreddy.g@gmail.com",
        //        EmailConfirmed = true,
        //        FirstName = "Abhi",
        //        LastName = "Reddy",
        //        DateOfBirth = DateTime.Now.AddYears(-30)
        //    };

        //    manager.Create(user, "MySuperP@ss!");

        //    if (!roleManager.Roles.Any())
        //    {
        //        roleManager.Create(new IdentityRole { Name = "SuperAdmin" });
        //        roleManager.Create(new IdentityRole { Name = "Admin" });
        //        roleManager.Create(new IdentityRole { Name = "User" });
        //    }

        //    var adminUser = manager.FindByName("SuperPowerUser");

        //    manager.AddToRoles(adminUser.Id, new string[] { "SuperAdmin", "Admin" });
        //}
    }
}
