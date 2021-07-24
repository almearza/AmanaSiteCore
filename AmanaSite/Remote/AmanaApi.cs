using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace AmanaSite.Remote
{
    public class AmanaApi
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly AmanaToken _token;

        public AmanaApi(HttpClient client, IConfiguration config, AmanaToken token)
        {
            _token = token;
            _config = config;
            client.BaseAddress = new Uri(config["AmanaApiUrl"]);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient = client;
        }
        public async Task<CounterVM> GetCounterAsync()
        {
            var token = await _token.GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync(_config["CountersUrl"]);

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var counter = await JsonSerializer.DeserializeAsync
                <CounterVM>(responseStream);
            return counter;
        }
    }
}