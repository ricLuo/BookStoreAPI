using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookStore.Data.Infrastructure;
using BookStore.Data.Repositories;
using BookStore.Models;
using BookStoreAPI.Filters;

namespace BookStoreAPI.Controllers
{
    [JwtAuthentication(Roles = "SuperAdmin,Admin")]
    [RoutePrefix("api/books")]
    public class BooksController : BaseApiController
    {
        private readonly IBooksRepository _booksRepository;

        public BooksController(ApplicationUserManager appUserManager, ApplicationRoleManager appRoleManager,
            IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        [HttpGet]
        [Route("{name}")]
        public HttpResponseMessage GetCategoryByName(string title)
        {
            var book = _booksRepository.GetBookByTitle(title);

            var response = book != null
                ? Request.CreateResponse(HttpStatusCode.OK, book)
                : Request.CreateResponse(HttpStatusCode.NotFound, "No Category Found for that name");
            return response;
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult SaveBook(Book book)
        {
            if (book == null && !ModelState.IsValid)
            {
                return BadRequest("Please enter valid information");
            }

            try
            {
                _booksRepository.Add(book);
                _booksRepository.SaveChanges();
                return Created(Request.RequestUri + "/" + book.Title, book);
            }
            catch (Exception)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet]
        [Route("{page:int?}")]
        public HttpResponseMessage GetAllBooks(int? page=0)
        {
            int totalCount = 0;
            int pageSize = 25;
            int skip;
            if (page != null)
            {
                 skip = page.Value * pageSize;
            }
            else
            {
                skip = 0;
            }
            IOrderedQueryable<Book> OrderBy(IQueryable<Book> queryable) => queryable.OrderBy(b => b.Title);


            var books = _booksRepository.GetQueryableData(out totalCount, null, OrderBy, null, skip, 25);
            var response = books.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, books)
                : Request.CreateResponse(HttpStatusCode.NotFound, "No Books Found");
            return response;
        }
    }
}