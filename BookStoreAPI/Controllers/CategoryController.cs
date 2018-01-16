using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BookStore.Data.Infrastructure;
using BookStore.Data.Repositories;
using BookStore.Models;
using BookStoreAPI.Filters;

namespace BookStoreAPI.Controllers
{
   // [JwtAuthentication]
    [RoutePrefix("api/categories")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class CategoryController : ApiController
    {
        private ApplicationUserManager _applicationUserManager;
        private ApplicationRoleManager _applicationRoleManager;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ApplicationUserManager appUserManager, ApplicationRoleManager appRoleManager,
            ICategoryRepository categoryRepository)
        {
            _applicationRoleManager = appRoleManager;
            _applicationUserManager = appUserManager;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAllCategories()
        {
            var categories = _categoryRepository.GetAll().OrderBy(c => c.Name).ToList();
            var response = categories.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, categories)
                : Request.CreateResponse(HttpStatusCode.NotFound, "No Categories Found");
            return response;
        }

        [HttpGet]
        [Route("{name}")]
        public HttpResponseMessage GetCategoryByName(string name)
        {
            var category = _categoryRepository.GetCategoryByName(name);

            var response = category != null
                ? Request.CreateResponse(HttpStatusCode.OK, category)
                : Request.CreateResponse(HttpStatusCode.NotFound, "No Category Found for that name");
            return response;
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult SaveCategory(Category category)
        {
            if (category == null && !ModelState.IsValid)
            {
                return BadRequest("Please enter valid information");
            }

            try
            {
                _categoryRepository.Add(category);
                _categoryRepository.SaveChanges();
                return Created(Request.RequestUri + "/" + category.Name, category);
            }
            catch (Exception)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }
    }
}
