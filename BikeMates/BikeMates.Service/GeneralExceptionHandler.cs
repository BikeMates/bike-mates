using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ExceptionHandling;
using BikeMates.Service.Controllers;

namespace BikeMates.Service
{
public class GeneralExceptionHandler : IExceptionHandler
{
    public virtual Task HandleAsync(ExceptionHandlerContext context,
                                        CancellationToken cancellationToken)
    {
        if (!ShouldHandle(context))
        {
            return Task.FromResult(0);
        }

            return HandleAsyncCore(context, cancellationToken);
        }

        public virtual Task HandleAsyncCore(ExceptionHandlerContext context,
                                            CancellationToken cancellationToken)
        {
            HandleCore(context);
            return Task.FromResult(0);
        }

        public virtual void HandleCore(ExceptionHandlerContext context)
        {
        }

        public virtual bool ShouldHandle(ExceptionHandlerContext context)
        {    
             return context.ExceptionContext.CatchBlock.IsTopLevel;
        }

    }

    public class OopsExceptionHandler : GeneralExceptionHandler
    {
        public override void HandleCore(ExceptionHandlerContext context)
        {
            context.Result = new TextPlainErrorResult
            {
                Request = context.ExceptionContext.Request,
                Content = "Oops! Something went wrong."
            };
        }

        private class TextPlainErrorResult : IHttpActionResult
        {
            public HttpRequestMessage Request { get; set; }
            public string Content { get; set; }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                HttpResponseMessage response =
                                 new HttpResponseMessage(HttpStatusCode.Redirect);
                response.Content = new StringContent(Content);
                response.Headers.Location = new Uri("http://localhost:51949/#error?=500");
                response.RequestMessage = Request;
                return Task.FromResult(response);
            }
        }
    }
}