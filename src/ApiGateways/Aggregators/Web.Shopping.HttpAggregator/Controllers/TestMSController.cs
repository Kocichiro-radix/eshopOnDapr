using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopOnContainers.Web.Shopping.HttpAggregator.Models;
using Microsoft.eShopOnDapr.Web.Shopping.HttpAggregator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopOnContainers.Web.Shopping.HttpAggregator.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TestMSController : ControllerBase
    {
        private readonly ITestMSService _testMSService;
        private readonly ICatalogService _catalog;

        public TestMSController(ICatalogService catalogService, ITestMSService testMSService)
        {
            _catalog = catalogService;
            _testMSService = testMSService;
        }

        [HttpGet]
        public async Task<IEnumerable<TodoItem>> GetTodoItems()
        {
            var todoList = await _testMSService.GetTodoItemsAsync();
            return todoList;
        }
    }
}
