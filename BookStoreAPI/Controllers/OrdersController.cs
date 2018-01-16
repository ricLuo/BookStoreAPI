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
    [JwtAuthentication(Roles = "User,Admin")]
    [RoutePrefix("api/orders")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    
    public class OrdersController : BaseApiController
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrdersController(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        [HttpGet]
        [Route("{page:int?}")]
        public IHttpActionResult GetAllOrders(int? page = 0)
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
            

            var orders = _ordersRepository.GetQueryableData(out totalCount, null, OrderBy, "OrderItems, Customer", skip, 25);
            var response = orders.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, orders)
                : Request.CreateResponse(HttpStatusCode.NotFound, "No Orders Found");
            return ResponseMessage(response);
        }

        // GET: api/Orders/5
        public string GetOrderById(int id)
        {
            return "value";
        }

        [HttpGet]
        [Route("customer/{customerId:int}")]
        public IHttpActionResult GetAllOrdersForCustomer(int customerId, int? page = 0 )
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

            Expression<Func<Order, bool>> filter = order => order.CustomerId == customerId.ToString();

            var orders = _ordersRepository.GetQueryableData(out totalCount, filter, OrderBy, "OrderItems, Customer", skip, 25);
            var response = orders.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, orders)
                : Request.CreateResponse(HttpStatusCode.NotFound, "No Orders Found for this Customer");
            return ResponseMessage(response);
        }

        // POST: api/Orders
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Orders/5
        public void Put(int id, [FromBody]string value)
        {
        }

        
        private static IOrderedQueryable<Order> OrderBy(IQueryable<Order> queryable)
        {
            return queryable.OrderByDescending(o=>o.CreatedDate);
        }
    }
}
