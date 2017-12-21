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

        public void InsertBookWithCategory(Book book)
        {
            var categoryIds = book.Categories.Select(c => c.Id).ToList();
            book.Categories = new List<Category>();
            foreach (var categoryId in categoryIds)
            {
                Category category = new Category() {Id = categoryId};
                Context.Categories.Attach(category);
                book.Categories.Add(category);
            }

            //  var categories = Context.Categories.Where(c => CategoryIds.Contains(c.Id)).ToList();
            // book.Categories = categories;
            Context.Books.Add(book);
            Context.SaveChanges();
        }
    }

    public interface IBooksRepository : IRepository<Book>
    {
        Book GetBookByTitle(string title);
        void InsertBookWithCategory(Book book);
    }
}