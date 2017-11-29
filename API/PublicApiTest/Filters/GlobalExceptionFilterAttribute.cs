using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace Iasset.Weatherapi.Filters
{
    public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute , IExceptionFilter
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //Log exception details into database or notify someone...

            var response = new HttpResponseMessage();
            response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            response.Content = new StringContent("Unhandled exception in the WebAPI.");
            response.ReasonPhrase = actionExecutedContext.Exception.Message;

            actionExecutedContext.Response = response;
        }
    }
}