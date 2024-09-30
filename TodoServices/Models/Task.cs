using System;
using System.Collections.Generic;

namespace TodoServices.Models;

public partial class Task
{
    public int TaskId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool Status { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public DateOnly CreatedDate { get; set; }

    public int? AssignedUserId { get; set; }

    public virtual UserList? AssignedUser { get; set; }

    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

    public virtual ICollection<Subtask> Subtasks { get; set; } = new List<Subtask>();

    public virtual ICollection<TaskLabel> TaskLabels { get; set; } = new List<TaskLabel>();
}
