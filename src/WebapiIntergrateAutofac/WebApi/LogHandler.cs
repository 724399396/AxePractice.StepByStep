using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi
{
    public class LogHandler : DelegatingHandler
    {
        const string StopWatchKey = "StopWatch";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var logger = (ILogger)request.GetDependencyScope().GetService(typeof(ILogger));
            request.Properties[StopWatchKey] = Stopwatch.StartNew();
            logger.Log($"{request.RequestUri} request begin;");

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            var stopWatch = (Stopwatch)request.Properties[StopWatchKey];

            logger.Log($"{request.RequestUri} " +
                       $" request used {stopWatch.ElapsedMilliseconds}ms to response");
            return response;
        }
    }
}