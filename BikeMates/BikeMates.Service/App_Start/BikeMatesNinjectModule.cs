using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BikeMates.Application.Services;
using BikeMates.Contracts.Repositories;
using BikeMates.Contracts.Services;
using BikeMates.DataAccess;
using BikeMates.DataAccess.Repository;
using Ninject.Modules;
using Ninject.Web.Common;

namespace BikeMates.Service
{
    public class BikeMatesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            this.Bind<BikeMatesDbContext>().To<BikeMatesDbContext>().InRequestScope();
            this.Bind<IUserRepository>().To<UserRepository>();
            this.Bind<IUserService>().To<UserService>();
            this.Bind<IRouteRepository>().To<RouteRepository>();
            this.Bind<IRouteService>().To<RouteService>();
        }
    }
}