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
