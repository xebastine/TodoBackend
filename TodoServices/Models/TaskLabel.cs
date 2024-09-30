using System;
using System.Collections.Generic;

namespace TodoServices.Models;

public partial class TaskLabel
{
    public int TaskId { get; set; }

    public int? LabelId { get; set; }

    public int TaskLabelIndex { get; set; }

    public virtual LabelList? Label { get; set; }

    public virtual Task Task { get; set; } = null!;
}
