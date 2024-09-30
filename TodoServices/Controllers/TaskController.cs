using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
//using a13=System.Threading.Tasks;
//using a12=TodoServices.Models;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using TodoServices.Models;

namespace TodoServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        [HttpGet]
        [Route("gettasklist")]
        public ActionResult Get()
        {
            using (TodoContext todocontext = new TodoContext())
            {
                var tasklist = 
                    todocontext.Tasks.
                    Include(s => s.Subtasks).
                    Include(s => s.TaskLabels).ToList();
                return StatusCode(200, tasklist);
            }
        }

        [HttpGet]
        [Route("gettasksbyid/{id}")]
        public ActionResult Get(int id)
        {
            using (TodoContext todocontext = new TodoContext())
            {
                var tasklist = todocontext.Tasks.Include(s => s.Subtasks).Include(s => s.TaskLabels).Where(a => a.TaskId == id).ToList().FirstOrDefault();
                if (tasklist != null) 
                {
                    return StatusCode(200, tasklist);
                }
                else
                    return StatusCode(404);
            }
        }

        [HttpPost]
        [Route("createtask")]
        public ActionResult Post([FromBody] Models.Task task)
        {
            using (TodoContext todocontext = new TodoContext())
            {
                if (task.EndTime == null)
                {
                    task.EndTime = DateTime.Now.AddDays(1);
                }
                if (task.StartTime > task.EndTime)
                    return StatusCode(400, "Set Task Ending Time to a time after Task Starting Time");
                todocontext.Tasks.Add(task);
                todocontext.SaveChanges();
                return StatusCode(200);
            }
        }

        [HttpPatch]
        [Route("taskcheckanduncheck")]
        public ActionResult patch(int id) {
            using (TodoContext todocontext = new TodoContext())
            {
                var task = todocontext.Tasks.Where(a => a.TaskId == id).FirstOrDefault();
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

        [HttpPatch]
        [Route("assignuser")]
        public ActionResult Patch(int id, [FromBody] int userid)
        {
            using (TodoContext todocontext = new TodoContext())
            {
                var task = todocontext.Tasks.Where(a => a.TaskId == id).FirstOrDefault();
                bool contains = todocontext.UserLists.Any(row => row.UserId == userid);
                if (contains == true)
                {
                    task.AssignedUserId = userid;
                    todocontext.SaveChanges();
                    return StatusCode(200);
                }
                else return StatusCode(404);
            }
        }

        [HttpDelete]
        [Route("deletetask")]
        public ActionResult delete(int id)
        {
            using (TodoContext todocontext = new TodoContext())
            { 
                var task = todocontext.Tasks.FirstOrDefault(a => a.TaskId == id);
                if (task != null)
                {
                    todocontext.Tasks.Remove(task);
                    todocontext.SaveChanges();
                    return StatusCode(200);
                }
                else
                {
                    return StatusCode(404);
                }
            }
        }
        
    }
}
