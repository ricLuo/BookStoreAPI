using BookStore.Data.Common;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace BookStore.Data.Infrastructure
{
    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole, string> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(BookStoreDbContext context)
        {
            var appDbContext = context;

            var appRoleManager = new ApplicationRoleManager(new RoleStore<IdentityRole>(appDbContext));

            return appRoleManager;
        }
    }
}