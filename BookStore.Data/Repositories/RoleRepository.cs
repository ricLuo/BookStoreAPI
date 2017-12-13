using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Data.Common;
using BookStore.Data.Infrastructure;
using BookStore.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookStore.Data.Repositories
{
   public class RoleRepository : Repository<IdentityRole>, IRoleRepository
    {
      
        private readonly ApplicationUserManager _appUserManager;
        private readonly ApplicationRoleManager _appRoleManager;

        public RoleRepository(BookStoreDbContext context, ApplicationUserManager appUserManager, ApplicationRoleManager appRoleManager) : base(context)
        {
            _appUserManager = appUserManager;
            _appRoleManager = appRoleManager;
        }
        public async Task<IdentityResult> CreateAsync(IdentityRole role)
        {
            return await _appRoleManager.CreateAsync(role);
        }

        public async Task<IdentityResult> DeleteAsync(IdentityRole role)
        {
            return await _appRoleManager.DeleteAsync(role);
        }

        public async Task<IdentityRole> FindByIdAsync(string roleId)
        {
            return await _appRoleManager.FindByIdAsync(roleId);
        }

        public async Task<IdentityRole> FindByNameAsync(string roleName)
        {
            return await _appRoleManager.FindByNameAsync(roleName);
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _appRoleManager.RoleExistsAsync(roleName);
        }

        public async Task<IdentityResult> UpdateAsync(IdentityRole role)
        {
            return await _appRoleManager.UpdateAsync(role);
        }
    }

   public interface IRoleRepository: IRepository<IdentityRole>
    {
        Task<IdentityResult> CreateAsync(IdentityRole role);
        Task<IdentityResult> DeleteAsync(IdentityRole role);
        Task<IdentityRole> FindByIdAsync(string roleId);
        Task<IdentityRole> FindByNameAsync(string roleName);
        Task<bool> RoleExistsAsync(string roleName);
        Task<IdentityResult> UpdateAsync(IdentityRole role);

    }
}
