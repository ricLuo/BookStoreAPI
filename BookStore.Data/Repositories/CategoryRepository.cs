using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Data.Common;
using BookStore.Models;

namespace BookStore.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(BookStoreDbContext context) : base(context)
        {
        }

        public Category GetCategoryByName(string name)
        {
            return Context.Categories.FirstOrDefault(c => c.Name == name);
        }
    }

    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetCategoryByName(string name);
    }
}