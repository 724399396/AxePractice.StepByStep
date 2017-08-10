using System.Diagnostics;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

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
            Logger.Log($"{actionExecutedContext.ActionContext.ActionDescriptor.ActionName} " +
                       $"action used {stopWatch.ElapsedMilliseconds}ms to response");
        }
    }
}