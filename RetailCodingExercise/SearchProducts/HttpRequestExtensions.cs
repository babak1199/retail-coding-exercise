using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Linq;

namespace RetailCodingExercise.SearchProducts
{
    public static class HttpRequestExtensions
    {
        public static string GetSearchQueryString(this HttpRequest request)
        {
            if (request == null) return null;

            string query = string.Empty;
            if (request.Query.TryGetValue("query", out StringValues queryVal))
            {
                query = queryVal.FirstOrDefault() ?? string.Empty;
            }

            return query;
        }
    }
}
