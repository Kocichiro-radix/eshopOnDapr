using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMSApi.Models;

namespace TestMSApi.Infrastructure.Repositories
{
    public interface IDaprMsTestStateRepository
    {
        Task<TodoItem> GetAsync(string id);
        Task<TodoItem> UpdateAsync(TodoItem todoItem);
        Task DeleteAsync(string id);
    }
}
