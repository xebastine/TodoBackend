using System;
using System.Collections.Generic;

namespace TodoServices.Models;

public partial class UserList
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
