using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookStore.Data.Common;
using BookStore.Data.Infrastructure;
using BookStore.Models;
using Microsoft.AspNet.Identity;

namespace BookStore.Data.Repositories
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationUserManager _appUserManager;
        private readonly ApplicationRoleManager _appRoleManager;

        public UserRepository(BookStoreDbContext context, ApplicationUserManager appUserManager,
            ApplicationRoleManager appRoleManager) : base(context)
        {
            _appUserManager = appUserManager;
            _appRoleManager = appRoleManager;
        }

        public async Task<ApplicationUser> GetUserByName(string username)
        {
            ApplicationUser user = await _appUserManager.FindByNameAsync(username);
            return user;
        }

        public async Task<ApplicationUser> FindByUserAsync(string username, string password)
        {
            ApplicationUser user = await _appUserManager.FindAsync(username, password);
            return user;
        }

        public async Task<ApplicationUser> FindByUserIdAsync(string id)
        {
            var user = await _appUserManager.FindByIdAsync(id);
            return user;
        }

        public async Task<IdentityResult> CreatUserAsync(ApplicationUser user, string password)
        {
            var addUserResult = await _appUserManager.CreateAsync(user, password);
            return addUserResult;
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string userId, string code)
        {
            var result = await _appUserManager.ConfirmEmailAsync(userId, code);
            return result;
        }

        public async Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var result = await _appUserManager.ChangePasswordAsync(userId, currentPassword, newPassword);
            return result;
        }

        public async Task<IdentityResult> DeleteUserAsync(ApplicationUser user)
        {
            var appUser = await _appUserManager.DeleteAsync(user);
            return appUser;
        }

        public async Task<IdentityResult> AssignRolesToUser(string userId, params string[] roles)
        {
            var appUser = await _appUserManager.FindByIdAsync(userId);
            var currentRoles = await _appUserManager.GetRolesAsync(userId);
            var rolesNotExists = roles.Except(_appRoleManager.Roles.Select(x => x.Name)).ToArray();

            if (rolesNotExists.Any())
            {
                //ModelState.AddModelError("", string.Format("Roles '{0}' does not exixts in the system", string.Join(",", rolesNotExists)));
                //return BadRequest(ModelState);

                return null;
            }

            var removeResult = await _appUserManager.RemoveFromRolesAsync(appUser.Id, currentRoles.ToArray());

            if (!removeResult.Succeeded)
            {
                // ModelState.AddModelError("", "Failed to remove user roles");
                // return BadRequest(ModelState);
            }

            var addResult = await _appUserManager.AddToRolesAsync(appUser.Id, roles);

            return !addResult.Succeeded ? null : addResult;
        }

       
    }

    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser> GetUserByName(string username);
        Task<ApplicationUser> FindByUserAsync(string username, string password);
        Task<ApplicationUser> FindByUserIdAsync(string id);
        Task<IdentityResult> CreatUserAsync(ApplicationUser user, string password);
        Task<IdentityResult> ConfirmEmailAsync(string userId, string code);
        Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
        Task<IdentityResult> DeleteUserAsync(ApplicationUser user);
        Task<IdentityResult> AssignRolesToUser(string userId, params string[] roles);
      //  ClaimsIdentity GenerateUserIdentityAsync(ApplicationUser manager);
    }
}