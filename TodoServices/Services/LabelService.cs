using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TodoServices.Interfaces;
using TodoServices.Models;

namespace TodoServices.Services
{
    public class LabelService : ILabelService
    {
        public TodoContext _todocontext;
        public LabelService(TodoContext context)
        {
             _todocontext = context;
        }

        public string AssignLabel(int taskid, int labelid)
        {
            bool taskexist = _todocontext.Tasks.Any(row => row.TaskId == taskid);
            bool labelexist = _todocontext.LabelLists.Any(row => row.LabelId == labelid);
            if (taskexist && labelexist)
            {
                TaskLabel tasklabel = new TaskLabel();
                tasklabel.TaskId = taskid;
                tasklabel.LabelId = labelid;
                _todocontext.TaskLabels.Add(tasklabel);
                _todocontext.SaveChanges();
                string msg = "Label assigned successfully";
                return msg;
            }
            else
            {
                string msg = "Error assigning label";
                return msg;
            }
        }

        public string UnassignLabel(int taskid, int labelid)
        {
            var tasklabel = _todocontext.TaskLabels.FirstOrDefault(row => row.LabelId == labelid && row.TaskId == taskid);
            if (tasklabel != null)
            {
                _todocontext.TaskLabels.Remove(tasklabel);
                _todocontext.SaveChanges();
                string msg = "Label removed successfully";
                return msg;
            }
            else
            {
                string msg = "Error removing label";
                return msg;
            }
        }

        public List<LabelList> GetLabelList()
        {
            var labellist = _todocontext.LabelLists.ToList();
            return labellist;
        }
    }
}
