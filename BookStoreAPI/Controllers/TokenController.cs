using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using BookStore.Data.Infrastructure;
using BookStore.Data.Repositories;
using BookStoreAPI.Infrastructure;
using BookStoreAPI.Models;
using Microsoft.AspNet.Identity;

namespace BookStoreAPI.Controllers
{
    //[RoutePrefix("api/token")]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class TokenController : ApiController
    {
        private ApplicationUserManager _applicationUserManager;
        private ApplicationRoleManager _applicationRoleManager;
        private readonly IUserRepository _userRepository;

        public TokenController(ApplicationUserManager appUserManager, ApplicationRoleManager appRoleManager,
            IUserRepository userRepository)
        {
            _applicationUserManager = appUserManager;
            _applicationRoleManager = appRoleManager;
            _userRepository = userRepository;
        }

        [Route("api/token")]
        [HttpPost]
        public async Task<IHttpActionResult> ValidateUserSendToken(LoginModel model)
        {
            var user = await _userRepository.FindByUserAsync(model.Username, model.Password);
            if (user == null)
            {
                return Content(HttpStatusCode.Unauthorized, "Invalid username or password");
            }
            var identity = _applicationUserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            return Ok(JwtManager.GenerateToken(user, identity));
        }
    }
}