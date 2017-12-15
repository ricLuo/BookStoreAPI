using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Controllers;
using BookStoreAPI.Infrastructure;

namespace BookStoreAPI.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class JwtAuthenticationAttribute : AuthorizeAttribute
    {
        public new bool AllowMultiple => false;
        public string Realm { get; set; }


        protected override bool IsAuthorized(HttpActionContext actionContext)
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
            var principal = AuthenticateJwtToken(token);

            if (principal == null)
                actionContext.Response =
                    actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new {Message = "Invalid Token"});

            else
                actionContext.RequestContext.Principal = principal;

            return base.IsAuthorized(actionContext);
        }


        private static bool ValidateToken(string token, out string username)
        {
            username = null;

            var simplePrinciple = JwtManager.GetPrincipal(token);

            if (!(simplePrinciple.Identity is ClaimsIdentity identity))
                return false;

            if (!identity.IsAuthenticated)
                return false;

            var usernameClaim = identity.FindFirst("userName");
            username = usernameClaim?.Value;

            if (string.IsNullOrEmpty(username))
                return false;

            // More validate to check whether username exists in system

            return true;
        }

        protected IPrincipal AuthenticateJwtToken(string token)
        {
            if (ValidateToken(token, out var username))
            {
                // based on username to get more information from database in order to build local identity
                var claims = new List<Claim>
                {
                    //new Claim(ClaimTypes.Name, username),
                    //new Claim(ClaimTypes.Role, "ssssss")
                    // Add more claims if needed: Roles, ...
                };

                var identity = new ClaimsIdentity(claims, "Jwt");
                IPrincipal user = new ClaimsPrincipal(identity);
                return user;
            }

            return null;
        }
    }
}