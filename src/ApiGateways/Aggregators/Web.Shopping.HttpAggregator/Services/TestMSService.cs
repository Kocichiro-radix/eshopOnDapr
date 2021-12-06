using Microsoft.eShopOnContainers.Web.Shopping.HttpAggregator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Microsoft.eShopOnDapr.Web.Shopping.HttpAggregator.Services
{
    public class TestMSService : ITestMSService
    {
        private readonly HttpClient _httpClient;

        public TestMSService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        Task<TodoItem> ITestMSService.GetTodoItemsAsync(int id)
        {
            var requestUri = $"TodoItem/{id}";
            return _httpClient.GetFromJsonAsync<TodoItem>(requestUri);
        }

        Task<IEnumerable<TodoItem>> ITestMSService.GetTodoItemsAsync()
        {
            var requestUri = $"TodoItem";
            return _httpClient.GetFromJsonAsync<IEnumerable<TodoItem>>(requestUri);
        }

    }
}
