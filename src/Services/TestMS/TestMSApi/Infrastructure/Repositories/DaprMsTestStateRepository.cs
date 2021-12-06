using Dapr.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMSApi.Models;

namespace TestMSApi.Infrastructure.Repositories
{
    public class DaprMsTestStateRepository : IDaprMsTestStateRepository
    {
        private const string StoreName = "eshop-statestore";

        private readonly ILogger<DaprMsTestStateRepository> _logger;
        private readonly DaprClient _dapr;

        public DaprMsTestStateRepository(ILoggerFactory loggerFactory, DaprClient dapr)
        {
            _logger = loggerFactory.CreateLogger<DaprMsTestStateRepository>();
            _dapr = dapr;
        }

        public async Task DeleteAsync(string id)
        {
            await _dapr.DeleteStateAsync(StoreName, id);
        }

        public async Task<TodoItem> GetAsync(string id)
        {
            return await _dapr.GetStateAsync<TodoItem>(StoreName, id);
        }

        public async Task<TodoItem> UpdateAsync(TodoItem todoItem)
        {
            var state = await _dapr.GetStateEntryAsync<TodoItem>(StoreName, todoItem.Id.ToString());
            state.Value = todoItem;

            await state.SaveAsync();

            _logger.LogInformation("MsTest item persisted successfully.");

            return await GetAsync(todoItem.Id.ToString());
        }
    }
}
