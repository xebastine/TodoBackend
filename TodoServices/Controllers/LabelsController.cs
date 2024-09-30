using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoServices.Interfaces;
using TodoServices.Models;

namespace TodoServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelsController : ControllerBase
    {
        public ILabelService _labelservice;
        public LabelsController(ILabelService labelservice)
        {
            _labelservice = labelservice;
        }

        [HttpPost]
        [Route("assignlabel")]
        public ActionResult Post(int taskid, int labelid)
        {
            try
            {
                string msg = _labelservice.AssignLabel(taskid, labelid);
                if (msg == "Label assigned successfully")
                {
                    return StatusCode(200, msg);
                }
                else
                    return StatusCode(404, msg);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpPost]
        [Route("unassignlabel")]
        public ActionResult Delete(int taskid, int labelid)
        {
            /*using (TodoContext todocontext = new TodoContext())
            {
                var tasklabel = todocontext.TaskLabels.FirstOrDefault(row => row.LabelId == labelid && row.TaskId == taskid);
                if (tasklabel!=null)
                {
                    todocontext.TaskLabels.Remove(tasklabel);
                    todocontext.SaveChanges();
                    return StatusCode(200);
                }
                else
                {
                    return StatusCode(404);
                }

            }*/
            try
            {
                string msg = _labelservice.UnassignLabel(taskid, labelid);
                if (msg == "Label removed successfully")
                {
                    return StatusCode(200, msg);
                }
                else
                    return StatusCode(404, msg);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        [Route("getlabellist")]
        public ActionResult Get()
        {
            using (TodoContext todocontext = new TodoContext())
            {
                var labellists = todocontext.LabelLists.ToList();
                return StatusCode(200, labellists);
            }
        }

        [HttpPost]
        [Route("createlabel")]
        public ActionResult Post(string name, string color)
        {
            using (TodoContext todocontext = new TodoContext())
            {
                LabelList labellist = new LabelList();
                labellist.LabelName = name;
                labellist.LabelColor = color;
                todocontext.Add(labellist);
                todocontext.SaveChanges();
                return StatusCode(200);
            }
        }

        [HttpDelete]
        [Route("deletelabel")]
        public ActionResult Delete(int id)
        {
            using (TodoContext todocontext = new TodoContext())
            {
                var label = todocontext.LabelLists.Where(a => a.LabelId == id).FirstOrDefault();
                if (label != null)
                {
                    todocontext.LabelLists.Remove(label);
                    todocontext.SaveChanges();
                    return StatusCode(200);
                }
                else
                    return StatusCode(404);
            }
        }
    }
}