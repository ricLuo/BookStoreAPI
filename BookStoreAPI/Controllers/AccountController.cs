using System;
using System.Threading.Tasks;
using System.Web.Http;
using BookStore.Data.Infrastructure;
using BookStore.Data.Repositories;
using BookStore.Models;
using BookStoreAPI.Filters;
using BookStoreAPI.Models;

namespace BookStoreAPI.Controllers
{
    [RoutePrefix("api/accounts")]
    public class AccountController : ApiController
    {
        private ApplicationUserManager _applicationUserManager;
        private ApplicationRoleManager _applicationRoleManager;
        private readonly IUserRepository _userRepository;

        public AccountController(ApplicationUserManager appUserManager, ApplicationRoleManager appRoleManager,
            IUserRepository userRepository)
        {
            _applicationUserManager = appUserManager;
            _applicationRoleManager = appRoleManager;
            _userRepository = userRepository;
        }

        [JwtAuthentication(Roles = "SuperAdmin,Admin")]
        [Route("create")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateUser(CreateUserBindingModel createUserModel)
        {
            if (createUserModel != null && !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser()
            {
                UserName = createUserModel.Username,
                Email = createUserModel.Email,
                FirstName = createUserModel.FirstName,
                LastName = createUserModel.LastName,
                DateOfBirth = createUserModel.DateOfBirth.Value
            };

            var addUserResult = await _userRepository.CreatUserAsync(user, createUserModel.Password);
            if (!addUserResult.Succeeded)
            {
                return InternalServerError();
            }

            var model = new UserModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                UserName = user.UserName
            };

            Uri locationHeader = new Uri(Url.Link("GetUserById", new {id = user.Id}));

            //string code = await this._userRepository.GenerateEmailConfirmationTokenAsync(user.Id);
            return Created(locationHeader, model);
        }

        [Route("user/{id:guid}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUser(string id)
        {
            //Only SuperAdmin or Admin can delete users (Later when implement roles)
            var user = await _userRepository.FindByUserIdAsync(id);

            if (user != null)
            {
                var model = new UserModel()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth,
                    UserName = user.UserName
                };

                return Ok(model);
            }

            return NotFound();
        }
    }
}