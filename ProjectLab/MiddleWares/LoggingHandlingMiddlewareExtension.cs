namespace ProjectLab.MiddleWares
{
    public static class LoggingHandlingMiddlewareExtension
    {
        public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingMiddleWare>();
        }
    }
}
