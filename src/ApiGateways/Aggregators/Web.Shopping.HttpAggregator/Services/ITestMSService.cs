using Microsoft.eShopOnContainers.Web.Shopping.HttpAggregator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopOnDapr.Web.Shopping.HttpAggregator.Services
{
    public interface ITestMSService
    {
        Task<IEnumerable<TodoItem>> GetTodoItemsAsync();
        Task<TodoItem> GetTodoItemsAsync(int id);
    }
}
