using System.Data.Entity;
using System.Reflection;
using BookStore.Data.Common;
using BookStore.Data.Repositories;
using BookStore.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;

namespace BookStoreAPI
{
    public class NinjectConfig
    {
        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            //Create the bindings
            kernel.Bind<ICategoryRepository>().To<CategoryRepository>();
            kernel.Bind<IAuthorsRepository>().To<AuthorsRepository>();
            kernel.Bind<IBooksRepository>().To<BooksRepository>();

            kernel.Bind<IUserStore<ApplicationUser>>().To<UserStore<ApplicationUser>>();
            kernel.Bind<UserManager<ApplicationUser>>().ToSelf();
            kernel.Bind<IRoleStore<IdentityRole, string>>().To<RoleStore<IdentityRole>>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IRoleRepository>().To<RoleRepository>();
            kernel.Bind<IOrdersRepository>().To<OrderRepository>();
            //kernel.Bind(typeof(IUserStore<>)).To(typeof(UserStore<>)).InRequestScope();
            kernel.Bind<DbContext>().To<BookStoreDbContext>();
            return kernel;
        }
    }
}