using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Routing;
using BookStore.Data.Infrastructure;
using BookStore.Data.Repositories;
using BookStoreAPI.Infrastructure;
using BookStoreAPI.Models;
using Microsoft.AspNet.Identity;

namespace BookStoreAPI.Controllers
{
    //[RoutePrefix("api/token")]
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
            var identity = _applicationUserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie );
           // user.Claims = identity.Claims;
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            return Ok(JwtManager.GenerateToken(user, identity));
        }
    }
}