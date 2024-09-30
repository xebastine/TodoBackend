using System;
using System.Collections.Generic;

namespace TodoServices.Models;

public partial class Attachment
{
    public int AttachmentId { get; set; }

    public string? AttachmentLink { get; set; }

    public int? TaskId { get; set; }

    public virtual Task? Task { get; set; }
}
