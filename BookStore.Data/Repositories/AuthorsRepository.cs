using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Data.Common;
using BookStore.Models;

namespace BookStore.Data.Repositories
{
    public class AuthorsRepository : Repository<Author>, IAuthorsRepository
    {
        public AuthorsRepository(BookStoreDbContext context) : base(context)
        {
        }
    }

    public interface IAuthorsRepository : IRepository<Author>
    {
    }
}