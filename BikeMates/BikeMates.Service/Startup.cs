using System;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using BikeMates.Service.Controllers;
using BikeMates.Service.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Ninject;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using BikeMates.DataAccess.Repository;
using BikeMates.DataAccess;
using BikeMates.DataAccess.Managers;
using BikeMates.Application.Services;

namespace BikeMates.Service
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var kernel = CreateKernel();
            ConfigureOAuth(app);
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
            config.DependencyResolver = new NinjectResolver(kernel);
            config.Services.Replace(typeof(IExceptionHandler), new OopsExceptionHandler());
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            var context = new BikeMatesDbContext();
            var userService = new UserService(new UserRepository(context, new UserManager(context)), new MailService());

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider(userService)
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }


        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load<BikeMatesNinjectModule>();
            return kernel;
        }
    }
}