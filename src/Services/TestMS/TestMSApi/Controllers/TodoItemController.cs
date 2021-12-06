using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMSApi.Infrastructure;
using TestMSApi.Infrastructure.Repositories;
using TestMSApi.Models;

namespace TestMSApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoItemController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly IDaprMsTestStateRepository _repository;
        public TodoItemController(TodoContext context, IDaprMsTestStateRepository repository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _repository = repository;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _context.TodoItems.ToListAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody]TodoItem item)
        {
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();
            return Ok(item.Id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var isExist = await _context.TodoItems.AnyAsync(a => a.Id == id);
            if (isExist)
            {
                var item = await _context.TodoItems.FirstOrDefaultAsync(a => a.Id == id);
                _context.TodoItems.Remove(item);
                var result = await _context.SaveChangesAsync();
                return Ok(result);
            }
            return Ok(0);
        }

        [Authorize(Policy = "ApiScope")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _repository.GetAsync(id.ToString());
            if(item == null)
            {
                item = await _context.TodoItems.FirstOrDefaultAsync(a => a.Id == id);
                await _repository.UpdateAsync(item);
            }
            return Ok(item);
        }
    }
}
