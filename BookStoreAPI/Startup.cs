﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BookStoreAPI.Startup))]

namespace BookStoreAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}