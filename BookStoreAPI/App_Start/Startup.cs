using System.Data.Entity;
using System.Web.Http;
using BookStore.Data.Common;
using BookStore.Data.Infrastructure;
using BookStoreAPI.Extensions;
using Owin;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;

namespace BookStoreAPI
{
    public partial class Startup
    {
        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            var webApiConfiguration = new HttpConfiguration();

            webApiConfiguration.EnableSwagger();

            WebApiConfig.Register(webApiConfiguration);

            app.CreatePerOwinContext(BookStoreDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            Database.SetInitializer<BookStoreDbContext>(null);
           // SwaggerConfig.Register(webApiConfiguration);

            app.UseNinjectMiddleware(NinjectConfig.CreateKernel);
            app.UseNinjectWebApi(webApiConfiguration);
        }
    }
}