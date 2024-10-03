using TodoServices.Models;

namespace TodoServices.Interfaces
{
    public interface ILabelService
    {
        public string AssignLabel(int taskid, int labelid);
        public string UnassignLabel(int taskid, int labelid);
        public List<LabelList> GetLabelList();
    }
}
