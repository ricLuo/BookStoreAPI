using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookStore.Data.Infrastructure;
using BookStore.Data.Repositories;
using BookStoreAPI.Filters;

namespace BookStoreAPI.Controllers
{
    [JwtAuthentication]
    [RoutePrefix("api/authors")]
    public class AuthorsController : ApiController
    {
        private ApplicationUserManager _applicationUserManager;
        private ApplicationRoleManager _applicationRoleManager;
        private readonly IAuthorsRepository _authorsRepository;

        public AuthorsController(ApplicationUserManager appUserManager, ApplicationRoleManager appRoleManager,
            IAuthorsRepository authorsRepository)
        {
            _applicationRoleManager = appRoleManager;
            _applicationUserManager = appUserManager;
            _authorsRepository = authorsRepository;
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAllAuthors()
        {
            var categories = _authorsRepository.GetAll().OrderBy(c => c.Name).ToList();
            var response = categories.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, categories)
                : Request.CreateResponse(HttpStatusCode.NotFound, "No Authors Found");
            return response;
        }
    }
}