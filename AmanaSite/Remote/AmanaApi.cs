using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace AmanaSite.Remote
{
    public class AmanaApi
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly AmanaToken _token;
        private readonly IMemoryCache _cache;

        public AmanaApi(HttpClient client, IConfiguration config, AmanaToken token, IMemoryCache cache)
        {
            this._cache = cache;
            _token = token;
            _config = config;
            client.BaseAddress = new Uri(config["AmanaApiUrl"]);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient = client;
        }
        public async Task<Counters> GetCounterAsync()
        {
            return await GetCachedCounterAsync();
        }
        public async Task<bool> ContactMun(ContactMun model)
        {
            try
            {
                var token = await _token.GetToken();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var reqBody = JsonSerializer.Serialize(model);
                var response = await _httpClient.PostAsync(_config["ContactusUrl"], new StringContent(reqBody,
                                                            Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
            }
            catch (System.Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> ContactInvest(ContactMun model)
        {
            try
            {
                var token = await _token.GetToken();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var reqBody = JsonSerializer.Serialize(model);
                var response = await _httpClient.PostAsync(_config["ContactInvestUrl"], new StringContent(reqBody,
                                                            Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
            }
            catch (System.Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> ContactAmeen(ContactMun model)
        {
            try
            {
                var token = await _token.GetToken();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var reqBody = JsonSerializer.Serialize(model);
                var response = await _httpClient.PostAsync(_config["ContactMayorUrl"], new StringContent(reqBody,
                                                            Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();
            }
            catch (System.Exception)
            {
                return false;
            }
            return true;
        }
        private async Task<Counters> GetCachedCounterAsync()
        {
            var cachecounter = await
                    _cache.GetOrCreateAsync(CacheKeys.AmanaApiCounters, async counter =>
                    {
                        counter.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24);
                        var _counter = await GetRemoteCounterAsync();
                        return _counter;
                    });
            return cachecounter;
        }
        private async Task<Counters> GetRemoteCounterAsync()
        {
            var token = await _token.GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync(_config["CountersUrl"]);

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var counter = await JsonSerializer.DeserializeAsync
                <Counters>(responseStream);
            return counter;
        }

    }
}