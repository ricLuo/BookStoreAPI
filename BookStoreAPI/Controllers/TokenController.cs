using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Http;
using BookStore.Data.Infrastructure;
using BookStore.Data.Repositories;
using BookStoreAPI.Infrastructure;

namespace BookStoreAPI.Controllers
{
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

        // THis is naive endpoint for demo, it should use Basic authentication to provide token or POST request
        [AllowAnonymous]
        public async Task<string> Get(string username, string password)
        {
            var user = await _userRepository.FindByUserAsync(username, password);

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            return JwtManager.GenerateToken(username);
        }

        
    }
}