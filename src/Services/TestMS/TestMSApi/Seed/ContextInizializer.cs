using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMSApi.Infrastructure;
using TestMSApi.Models;

namespace TestMSApi.Seed
{
    internal class ContextInizializer
    {
        internal static void Seed(TodoContext todoContext)
        {
            if(todoContext.TodoItems.Count() < 1)
            {
                var todo1 = new TodoItem()
                {
                    Name = "test1"
                };
                var todo2 = new TodoItem()
                {
                    Name = "test2"
                };
                var todo3 = new TodoItem()
                {
                    Name = "test3"
                };
                todoContext.Add(todo1);
                todoContext.Add(todo2);
                todoContext.Add(todo3);
                todoContext.SaveChanges();
            }
        }
    }
}
