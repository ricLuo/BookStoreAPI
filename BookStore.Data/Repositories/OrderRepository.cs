using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Data.Common;
using BookStore.Models;

namespace BookStore.Data.Repositories
{
   public class OrderRepository: Repository<Order>, IOrdersRepository
    {
        public OrderRepository(BookStoreDbContext context) : base(context)
        {
        }
    }

    public interface IOrdersRepository : IRepository<Order>
    {
        
    }
}
