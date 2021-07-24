using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace AmanaSite.Remote
{
    public class AmanaToken
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _config;
        public AmanaToken(IMemoryCache cache, HttpClient client, IConfiguration config)
        {
            _config = config;
            client.BaseAddress = new Uri(config["AmanaApiUrl"]);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            _httpClient = client;
            _cache = cache;

        }
        public async Task<string> GetToken()
        {
            return await GetCachedToken();
        }
        private async Task<string> GetCachedToken()
        {
            var cacheToken = await
                    _cache.GetOrCreateAsync(CacheKeys.AmanaApiToken, async token =>
                    {
                        token.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(50);
                        var _token = await GetRemoteTokenAsync();
                        return _token.access_token;
                    });
            return cacheToken;
        }
        private async Task<Token> GetRemoteTokenAsync()
        {
            var requestContent = new FormUrlEncodedContent(new[]
             {
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            });

            var username = _config["AmanaApiUser"];
            var pass = _config["AmanaApiPass"];
            var authenticationString = $"{username}:{pass}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, _config["AmanaApiTokenUrl"]);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            requestMessage.Content = requestContent;

            using var httpResponse =
            await _httpClient.SendAsync(requestMessage);
            
            httpResponse.EnsureSuccessStatusCode();

            using var responseStream = await httpResponse.Content.ReadAsStreamAsync();
            var token = await JsonSerializer.DeserializeAsync<Token>(responseStream);
            return token;
        }
    }
}