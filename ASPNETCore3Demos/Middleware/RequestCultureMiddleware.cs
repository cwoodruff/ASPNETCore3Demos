using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Middleware
{
    public class RequestCultureMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RequestCultureOptions _options;

        public RequestCultureMiddleware(RequestDelegate next, IOptions<RequestCultureOptions> options)
        {
            _next = next;
            _options = options.Value;
        }

        public Task Invoke(HttpContext httpContext)
        {
            CultureInfo requestCulture = null;

            var cultureQuery = httpContext.Request.Query["culture"];
            if (!string.IsNullOrWhiteSpace(cultureQuery))
                requestCulture = new CultureInfo(cultureQuery);
            else
                requestCulture = _options.DefaultCulture;

            if (requestCulture != null)
            {
                CultureInfo.CurrentCulture = requestCulture;
                CultureInfo.CurrentUICulture = requestCulture;
            }

            return _next(httpContext);
        }
    }

    public static class RequestCultureMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestCulture(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestCultureMiddleware>();
        }

        public static IApplicationBuilder UseRequestCulture(this IApplicationBuilder builder, RequestCultureOptions options)
        {
            return builder.UseMiddleware<RequestCultureMiddleware>(Options.Create(options));
        }
    }
}