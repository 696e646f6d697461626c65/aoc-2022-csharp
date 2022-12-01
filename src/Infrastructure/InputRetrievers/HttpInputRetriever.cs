using System.Data;

using AOC2022.Domain;
using AOC2022.Domain.Common;

using Microsoft.Extensions.Caching.Memory;

namespace AOC2022.Infrastructure.InputRetrievers
{
    public class HttpInputRetriever : IInputRetriever
    {
        private const string Host = "adventofcode.com";
        private const int Year = 2021;
        private readonly HttpClient _httpClient;

        public HttpInputRetriever(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async IAsyncEnumerable<string> GetInputForDay(ValidDayNumber day)
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = "https",
                Host = Host,
                Path = $"{Year}/day/{day.Value}/input"
            };

            using var request = new HttpRequestMessage();

            request.RequestUri = uriBuilder.Uri;

            request.Method = HttpMethod.Get;

            var response = await _httpClient.SendAsync(
                request,
                HttpCompletionOption.ResponseHeadersRead);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());
                yield break;
            }

            var responseContentStream = await response.Content.ReadAsStreamAsync();

            using var reader = new StreamReader(responseContentStream);

            string? line;

            while ((line = await reader.ReadLineAsync()) != null)
            {
                yield return line;
            }
        }
    }
}