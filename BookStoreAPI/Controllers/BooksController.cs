using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BookStore.Data.Repositories;
using BookStore.Models;
using BookStoreAPI.Filters;

namespace BookStoreAPI.Controllers
{
   // [JwtAuthentication(Roles = "SuperAdmin,Admin")]
    [RoutePrefix("api/books")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class BooksController : ApiController
    {
        private readonly IBooksRepository _booksRepository;
        private readonly ICategoryRepository _categoryRepository;

        public BooksController(IBooksRepository booksRepository, ICategoryRepository categoryRepository)
        {
            _booksRepository = booksRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [Route("{name}")]
        public HttpResponseMessage GetBookById(int id)
        {
            var book = _booksRepository.GetById(id);

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
                _booksRepository.InsertBookWithCategory(book);
                return Created(Request.RequestUri + "/" + book.Id, book);
            }
            catch (Exception)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet]
        [Route("{page:int?}")]
        public HttpResponseMessage GetAllBooks(int? page = 0, string title = "")
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

            Expression<Func<Book, bool>> filter = book => book.Title.Contains(title);

            var books = _booksRepository.GetQueryableData(out totalCount, filter, OrderBy, "Categories", skip, 25);
            var response = books.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, books)
                : Request.CreateResponse(HttpStatusCode.NotFound, "No Books Found");
            return response;
        }

        private IOrderedQueryable<Book> OrderBy(IQueryable<Book> queryable)
        {
            return queryable.OrderByDescending(b => b.Rating);
        }
    }
}