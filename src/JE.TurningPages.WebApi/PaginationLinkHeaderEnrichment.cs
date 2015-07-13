using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using JE.TurningPages.Contracts;
using Newtonsoft.Json;

namespace JE.TurningPages.WebApi
{
    public class PaginationLinkHeaderEnrichment : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (request.RequestUri.ToString().Contains("favicon.ico")) {
                return base.SendAsync(request, cancellationToken);
            }
            var response = base.SendAsync(request, cancellationToken);
            return EnrichWithPagingLinkHeader(request, response);
        }

        private static Task<HttpResponseMessage> EnrichWithPagingLinkHeader(
            HttpRequestMessage request,
            Task<HttpResponseMessage> response)
        {
            if (!ShouldEnrichResponse(request, response)) { return response; }
            var pagingInfo = ReadPaginationInfo(request);
            var link = BuildLinkHeader(request, pagingInfo);
            AddHeaderToResponse(PageRequest.LinkHeader, link, response);

            var xpagination = BuildXPaginationHeader(pagingInfo);
            AddHeaderToResponse(PageRequest.XPaginationHeader, xpagination, response);

            return response;
        }

        private static PaginationInfo ReadPaginationInfo(HttpRequestMessage request)
        {
            return request.Properties[PaginationInfo.PropertyKey] as PaginationInfo;
        }

        private static bool ShouldEnrichResponse(HttpRequestMessage request, Task<HttpResponseMessage> response)
        {
            return !IsErrorResponse(response) && NeedsLinkHeader(request)
                   && PageInfoIsPresentInRequestProperties(request);
        }

        private static bool PageInfoIsPresentInRequestProperties(HttpRequestMessage request)
        {
            if (!request.Properties.ContainsKey(PaginationInfo.PropertyKey)) { return false; }
            var value = request.Properties[PaginationInfo.PropertyKey] as PaginationInfo;
            return value != null;
        }

        private static bool IsErrorResponse(Task<HttpResponseMessage> response)
        {
            return !response.Result.IsSuccessStatusCode;
        }

        private static bool NeedsLinkHeader(HttpRequestMessage request)
        {
            var query = request.RequestUri.ParseQueryString();
            return !string.IsNullOrWhiteSpace(query["page"]);
        }

        private static string BuildLinkHeader(HttpRequestMessage request, PaginationInfo pi)
        {
            var path = request.RequestUri.AbsolutePath;
            var buffer = new List<string>();
            if (NeedsNextPage(pi))
            {
                buffer.Add(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "next: <{0}?page={1}&pageSize={2}>",
                        path,
                        pi.Page + 1,
                        pi.PageSize));
            }
            if (NeedsPreviousPage(pi))
            {
                buffer.Add(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "previous: <{0}?page={1}&pageSize={2}>",
                        path,
                        pi.Page - 1,
                        pi.PageSize));
            }
            if (NeedsLastPage(pi))
            {
                buffer.Add(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "last: <{0}?page={1}&pageSize={2}>",
                        path,
                        pi.TotalPages,
                        pi.PageSize));
            }
            return string.Join("; ", buffer.ToArray());
        }

        private static bool NeedsLastPage(PaginationInfo pi)
        {
            return NeedsNextPage(pi) && !IsLastPage(pi);
        }

        private static bool IsLastPage(PaginationInfo pi)
        {
            return pi.Page >= pi.TotalPages;
        }

        private static bool NeedsPreviousPage(PaginationInfo pi)
        {
            return pi.Page > 1;
        }

        private static bool NeedsNextPage(PaginationInfo pi)
        {
            return pi.Page < pi.TotalPages;
        }

        private static string BuildXPaginationHeader(PaginationInfo pi)
        {
            return JsonConvert.SerializeObject(pi, Formatting.None);
        }

        private static void AddHeaderToResponse(
            string headerName,
            string headerValue,
            Task<HttpResponseMessage> response)
        {
            response.Result.Headers.Add(headerName, headerValue);
        }
    }
}
