using System.Collections.Generic;
using System.Linq;
using BookStore.Data.Common;
using BookStore.Models;

namespace BookStore.Data.Repositories
{
    public class BooksRepository : Repository<Book>, IBooksRepository
    {
        public BooksRepository(BookStoreDbContext context) : base(context)
        {
        }

        public Book GetBookByTitle(string title)
        {
            var book = Context.Books.FirstOrDefault(b => b.Title == title);
            return book;
        }
    }

    public interface IBooksRepository : IRepository<Book>
    {
        Book GetBookByTitle(string title);
    }
}