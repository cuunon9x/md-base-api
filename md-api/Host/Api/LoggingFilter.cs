using log4net;
using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Api
{
    /// <summary>
    /// Logs requests and responses.
    /// </summary>
    public class LoggingFilterAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        ILog log;

        public Func<string, string> ResponseContentSanitizer = null;

        /// <summary>
        /// Log start of request handling.
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // Todo
            base.OnActionExecuting(actionContext);
        }

        /// <summary>
        /// Log end of request handling.
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            // Todo
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}
