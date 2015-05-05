using ADSecuredWebApi.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Web.Http;

namespace ADSecuredWebApi.Controllers
{
    [Authorize]
    public class TodoListController : ApiController
    {
        // ToDo items list for all users
        static ConcurrentBag<ToDoItem> todoBag = new ConcurrentBag<ToDoItem>();

        // GET api/todolist returns 
        public IEnumerable<ToDoItem> Get()
        {
            return todoBag;
        }

        // POST api/todolist
        public void Post(ToDoItem todo)
        {
            if (null != todo && !string.IsNullOrWhiteSpace(todo.Title))
            {
                todoBag.Add(new ToDoItem { Title = todo.Title });
            }
        }
    }
}
