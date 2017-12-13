using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Data.Common;
using BookStore.Models;

namespace BookStore.Data.Repositories
{
   public class BooksRepository: Repository<Book>, IBooksRepository
    {
        public BooksRepository(BookStoreDbContext context) : base(context)
        {
        }
    }

   public interface IBooksRepository:IRepository<Book>
    {
        
    }
}
