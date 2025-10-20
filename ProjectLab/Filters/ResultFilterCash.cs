using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace ProjectLab.Filters
{
    public class ResultFilterCash : Attribute, IResultFilter
    {
        private static readonly MemoryCache _memoryCache = new(new MemoryCacheOptions());
        private const string CacheKey = "DefaultCacheKey";
        private const int DurationInSeconds = 60;

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (_memoryCache.TryGetValue(CacheKey, out var cachedResult))
            {
                context.Result = new JsonResult(cachedResult);
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            if (context.Result is ObjectResult result)
            {
                _memoryCache.Set(CacheKey, result.Value, TimeSpan.FromSeconds(DurationInSeconds));
            }
        }
    }
}
