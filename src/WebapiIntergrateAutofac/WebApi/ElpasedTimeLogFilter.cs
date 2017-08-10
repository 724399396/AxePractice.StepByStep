using System.Diagnostics;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Filters;
using Test;

namespace WebApi
{
    public class ElpasedTimeLogFilter : ActionFilterAttribute
    {
        private const string StopWatchKey = "StopWatch";

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
            actionContext.Request.Properties[StopWatchKey] = Stopwatch.StartNew();
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
            Stopwatch stopWatch = (Stopwatch) actionExecutedContext.Request.Properties[StopWatchKey];
            IDependencyScope dependencyScope = actionExecutedContext.Request.GetDependencyScope();
            var logger = (ILogger) dependencyScope.GetService(typeof(ILogger));
            logger.Log($"{actionExecutedContext.ActionContext.ActionDescriptor.ActionName} " +
                       $"action used {stopWatch.ElapsedMilliseconds}ms to response");
        }
    }
}