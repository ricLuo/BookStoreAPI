using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using BookStore.Data.Repositories;
using BookStoreAPI.Infrastructure;
using Ninject;

namespace BookStoreAPI.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class JwtAuthenticationAttribute : AuthorizeAttribute
    {
        public new bool AllowMultiple => false;
        public string Realm { get; set; }

        [Inject]
        public IUserRepository UserRepository { get; set; }


        public override async Task OnAuthorizationAsync(HttpActionContext actionContext,
            CancellationToken cancellationToken)
        {
            var request = actionContext.Request;
            var authorization = request.Headers.Authorization;

            if (authorization == null || authorization.Scheme != "Bearer")
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest,
                    new {Message = "Missing JWT Token"});

            if (string.IsNullOrEmpty(authorization.Parameter))
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized,
                    new {Message = "Missing JWT Token"});
            }

            var token = authorization.Parameter;
            var principal = await AuthenticateJwtTokenAsync(token);

            //  var isAuthorized = _allowedRoles.Any(x => Roles.Contains(x));

            if (principal == null)
                actionContext.Response =
                    actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new {Message = "Invalid Token"});

            actionContext.RequestContext.Principal = principal;
            //Thread.CurrentPrincipal.Identity.Name = principal.Identity.Name;
        }


        protected async Task<IPrincipal> AuthenticateJwtTokenAsync(string token)
        {
            var jwTPrinciple = ValidateTokenAndGetprinciple(token, out var userName);

            if (jwTPrinciple == null) return null;
            // based on username to get more information from database in order to build local identity
            var appUser = await UserRepository.GetUserByName(userName);

            if (appUser == null) return null;
            var roles = await UserRepository.GetRolesAsync(appUser.Id);
            if (roles == null || !(jwTPrinciple.Identity is ClaimsIdentity identityTest)) return null;
            if (!string.IsNullOrEmpty(Roles))
            {
                var allowedRoles = Roles.Split(',');
                var jwtRoles = identityTest.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
                var rolesAreEqual = CompareRoles(roles.ToArray(), jwtRoles.ToArray());
                if (!rolesAreEqual) return null;

                var isAuthorized = allowedRoles.Intersect(jwtRoles);
                IPrincipal user = isAuthorized.Any() ? new ClaimsPrincipal(jwTPrinciple.Identity) : null;
                
                return user;
            }
            else
            {
                var jwtRoles = identityTest.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
                var rolesAreEqual = CompareRoles(roles.ToArray(), jwtRoles.ToArray());
                if (!rolesAreEqual) return null;
                IPrincipal user = new ClaimsPrincipal(jwTPrinciple.Identity);

                return user;
            }
           
        }

        private static bool CompareRoles(string[] dbRoles, string[] jwtRoles)
        {
            return dbRoles.All(jwtRoles.Contains);
        }


        private static ClaimsPrincipal ValidateTokenAndGetprinciple(string token, out string username)
        {
            username = null;

            var simplePrinciple = JwtManager.GetPrincipal(token);

            if (!(simplePrinciple.Identity is ClaimsIdentity identity))
                return null;

            if (!identity.IsAuthenticated)
                return null;

            var usernameClaim = identity.FindFirst("userName");
            username = usernameClaim?.Value;

            if (string.IsNullOrEmpty(username))
                return null;

            // More validate to check whether username exists in system

            return simplePrinciple;
        }
    }
}