namespace GameFrontEnd.Mvc.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    _logger.LogError("{ExceptionType} {ExcpetionMessage}", ex.InnerException.GetType().ToString(),
                        ex.InnerException.Message);
                }
                else
                {
                    _logger.LogError("{ExceptionType} {ExcpetionMessage}", ex.GetType().ToString(),
                       ex.Message);
                }
                //httpContext.Response.StatusCode = 500;
                //await httpContext.Response.WriteAsync("Error Occurred. Please try again");

                //rethrowing the error to build in exception handler
                throw;
            }
        }
    }
    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
