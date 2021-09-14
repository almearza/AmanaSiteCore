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
                        token.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(365);
                        var _token = await GetRemoteTokenAsync();
                        return _token;
                    });
            return cacheToken;
        }
        private async Task<string> GetRemoteTokenAsync()
        {
            // var requestContent = new FormUrlEncodedContent(new[]
            //  {
            //     new KeyValuePair<string, string>("grant_type", "client_credentials")
            // });

            // var pass = _config["AmanaApiPass"];
            // var authenticationString = $"{username}:{pass}";
            // var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

            // var requestMessage = new HttpRequestMessage(HttpMethod.Post, _config["AmanaApiTokenUrl"]);
            // requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            // requestMessage.Content = requestContent;

            // using var httpResponse =
            // await _httpClient.SendAsync(requestMessage);

             var apiKey = _config["AmanaApiKey"];
             var reqBody = JsonSerializer.Serialize(new 
            {
                DeveloperKey = apiKey
            });
            using var httpResponse =
            await _httpClient.PostAsync(_config["AmanaApiTokenUrl"],new StringContent(reqBody,Encoding.UTF8, "application/json"));
            
            httpResponse.EnsureSuccessStatusCode();

            using var responseStream = await httpResponse.Content.ReadAsStreamAsync();
            var token = await JsonSerializer.DeserializeAsync<string>(responseStream);
            return token;
        }
    }
}