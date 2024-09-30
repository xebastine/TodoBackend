using System;
using System.Collections.Generic;

namespace TodoServices.Models;

public partial class Subtask
{
    public int? TaskId { get; set; }

    public int SubTaskId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool Status { get; set; }

    public DateOnly CreatedDate { get; set; }

    public virtual Task? Task { get; set; }
}
