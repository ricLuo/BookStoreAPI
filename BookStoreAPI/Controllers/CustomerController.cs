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
    [JwtAuthentication(Roles = "User,SuperAdmin,Admin")]
    [RoutePrefix("api/Customer")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class CustomerController : ApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrdersRepository _ordersRepository;

        public CustomerController(IUserRepository userRepository, IOrdersRepository ordersRepository)
        {
            _userRepository = userRepository;
            _ordersRepository = ordersRepository;
        }

        [HttpGet]
        [Route("orders/{id:int?}/{page:int?}")]
        public IHttpActionResult GetAllOrders(int id, int? page = 0)
        {
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

            Expression<Func<Order, bool>> filter = order => order.CustomerId == id.ToString();

            var orders =
                _ordersRepository.GetQueryableData(out _, filter, OrderBy, "OrderItems, Customer", skip, 25);
            var response = orders.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, orders)
                : Request.CreateResponse(HttpStatusCode.NotFound, "No Orders Found for this Customer");
            return ResponseMessage(response);
        }

        private static IOrderedQueryable<Order> OrderBy(IQueryable<Order> queryable)
        {
            return queryable.OrderBy(o => o.Id);
        }
    }
}