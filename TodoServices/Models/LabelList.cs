using System;
using System.Collections.Generic;

namespace TodoServices.Models;

public partial class LabelList
{
    public int LabelId { get; set; }

    public string LabelName { get; set; } = null!;

    public string? LabelColor { get; set; }

    public virtual ICollection<TaskLabel> TaskLabels { get; set; } = new List<TaskLabel>();
}
