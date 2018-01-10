using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using BookStore.Data.Infrastructure;
using BookStore.Data.Repositories;
using BookStoreAPI.Filters;
using BookStoreAPI.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookStoreAPI.Controllers
{
    [RoutePrefix("api/roles")]
    [JwtAuthentication]
    public class RolesController : ApiController
    {
        private readonly ApplicationRoleManager _applicationRoleManager;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        public RolesController(ApplicationRoleManager appRoleManager, IRoleRepository roleRepository,
            IUserRepository userRepository, ApplicationUserManager appUserManager)
        {
            _applicationRoleManager = appRoleManager;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        [Route("", Name = "GetAllRoles")]
        public IHttpActionResult GetAllRoles()
        {
            var roles = this._applicationRoleManager.Roles;

            return Ok(roles);
        }

        [Route("{id:guid}", Name = "GetRoleById")]
        public async Task<IHttpActionResult> GetRole(string id)
        {
            var role = await this._roleRepository.FindByIdAsync(id);

            if (role != null)
            {
                return Ok(TheModelFactory.Create(role));
            }

            return NotFound();
        }

        [Route("create")]
        public async Task<IHttpActionResult> Create(CreateRoleBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = new IdentityRole {Name = model.Name};
            var result = await this._roleRepository.CreateAsync(role);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            Uri locationHeader = new Uri(Url.Link("GetRoleById", new {id = role.Id}));
            return Created(locationHeader, role);
        }


        [Route("create/UserRoles")]
        [HttpPost]
        public async Task<IHttpActionResult> AssignRolesToUser(CreateUserRolesBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var result = await this._userRepository.AssignRolesToUser(model.UserId, model.Roles);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            Uri locationHeader = new Uri(Url.Link("GetUserRoles", new {id = model.UserId }));
            return Created(locationHeader, result);
        }

        [Route("user/{id:guid}", Name = "GetUserRoles")]
        public async Task<IHttpActionResult> GetUserRoles(string id)
        {
            var user = await _userRepository.GetRolesAsync(id);
            
            if (user != null && user.Any())
            {
               return Ok(user);
            }

            return NotFound();
        }
    }
}