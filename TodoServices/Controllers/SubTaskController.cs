using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoServices.Models;

namespace TodoServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubTaskController : ControllerBase
    {
        [HttpPost]
        [Route("createsubtask")]
        public ActionResult Post(int id, [FromBody] Subtask subtask)
        {
            using (TodoContext todocontext = new TodoContext())
            {
                bool exist = todocontext.Tasks.Any(row => row.TaskId == id);
                if (exist)
                {
                    subtask.TaskId = id;
                    todocontext.Subtasks.Add(subtask);
                    todocontext.SaveChanges();
                    return StatusCode(200);
                }
                else return StatusCode(404);
            }
        }

        [HttpGet]
        [Route("getsubtaskbyid")]
        public ActionResult Get(int id)
        {
            using (TodoContext todocontext = new TodoContext())
            {
                var subtasks = todocontext.Subtasks.Where(row => row.TaskId == id).ToList();
                if (subtasks.Count > 0)
                    return StatusCode(200, subtasks);
                else
                    return StatusCode(404, "No subtasks created for this task");
            }
        }

        [HttpDelete]
        [Route("deletesubtask")]
        public ActionResult Delete(int id)
        {
            using (TodoContext todocontext = new TodoContext())
            { 
                var subtask = todocontext.Subtasks.FirstOrDefault(a=>a.SubTaskId == id);
                if (subtask == null)
                {
                    return StatusCode(404, "Subtask not found");
                }
                else
                {
                    todocontext.Subtasks.Remove(subtask);
                    todocontext.SaveChanges();
                    return StatusCode(200);
                }

            }
        }
        [HttpPatch]
        [Route("subtaskcheckanduncheck")]
        public ActionResult patch(int id)
        {
            using (TodoContext todocontext = new TodoContext())
            {
                var task = todocontext.Subtasks.Where(a => a.SubTaskId == id).FirstOrDefault();
                if (task.Status == false)
                {
                    task.Status = true;
                    todocontext.SaveChanges();
                    return StatusCode(200);
                }
                else
                {
                    task.Status = false;
                    todocontext.SaveChanges();
                    return StatusCode(200);
                }
            }
        }
    }
}
