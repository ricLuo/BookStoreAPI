using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookStore.Data.Infrastructure;
using BookStore.Data.Repositories;
using BookStore.Models;
using BookStoreAPI.Filters;

namespace BookStoreAPI.Controllers
{
    //[JwtAuthentication]
    [RoutePrefix("api/authors")]
    public class AuthorsController : ApiController
    {
        private readonly IAuthorsRepository _authorsRepository;

        public AuthorsController(ApplicationUserManager appUserManager, ApplicationRoleManager appRoleManager,
            IAuthorsRepository authorsRepository)
        {
            _authorsRepository = authorsRepository;
        }

        [HttpGet]
        [Route("{page:int?}")]
        public HttpResponseMessage GetAllAuthors(int? page = 0, string title = "")
        {
            int totalCount = 0;
            int pageSize = 25;
            int skip;
            if (page.HasValue && page > 1)
            {
                skip = page.Value * pageSize;
            }
            else
            {
                skip = 0;
            }
            Expression<Func<Author, bool>> filter = author => author.Name.Contains(title);

            var authors = _authorsRepository.GetQueryableData(out totalCount, filter, OrderBy, null, skip, 25);
            var response = authors.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, authors)
                : Request.CreateResponse(HttpStatusCode.NotFound, "No Authors Found");
            return response;
        }

        private IOrderedQueryable<Author> OrderBy(IQueryable<Author> queryable)
        {
            return queryable.OrderBy(a => a.Name);
        }
    }
}