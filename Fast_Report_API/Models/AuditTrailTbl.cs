using System;
using System.Collections.Generic;

namespace Fast_Report_API.Models;

public partial class AuditTrailTbl
{
    public ulong AuditTrialId { get; set; }

    public ulong UserId { get; set; }

    public string? Action { get; set; }

    public string? Description { get; set; }

    public string? Type { get; set; }

    public string? IpAddress { get; set; }

    public string? Metadata { get; set; }

    public int? EncodedBy { get; set; }

    public DateTime? EncodedDate { get; set; }

    public virtual UsersTbl User { get; set; } = null!;
}
