using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RetailCodingExercise.Filters
{
    public class ExecutionTimeAsyncFilter : IAsyncActionFilter
    {
        private readonly Stopwatch stopwatch = new Stopwatch();
        private readonly ILogger<ExecutionTimeAsyncFilter> _logger;

        public ExecutionTimeAsyncFilter(ILogger<ExecutionTimeAsyncFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            stopwatch.Start();

            var result = await next();

            stopwatch.Stop();

            Log(context);
        }

        private void Log(ActionExecutingContext context)
        {
            var controllerName = context.RouteData.Values["controller"];
            var actionName = context.RouteData.Values["action"];

            if (!stopwatch.IsRunning)
            {
                _logger.LogInformation($"[{controllerName}/{actionName}] Ran in {{elapsedMilliseconds}} ms", stopwatch.Elapsed.TotalMilliseconds);
            }
        }
    }
}
