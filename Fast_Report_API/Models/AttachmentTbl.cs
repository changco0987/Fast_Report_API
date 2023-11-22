using System;
using System.Collections.Generic;

namespace Fast_Report_API.Models;

public partial class AttachmentTbl
{
    public ulong AttachmentId { get; set; }

    public ulong NoaId { get; set; }

    public string? Attachment { get; set; }

    public string? Type { get; set; }

    public int? EncodedBy { get; set; }

    public DateTime? EncodedDate { get; set; }

    public virtual NoaTbl Noa { get; set; } = null!;
}
