using System;
using System.Collections.Generic;

namespace Fast_Report_API.Models;

public partial class PositionTbl
{
    public ulong PositionId { get; set; }

    public string PositionName { get; set; } = null!;

    public ulong StatusId { get; set; }

    public int? EncodedBy { get; set; }

    public DateTime? EncodedDate { get; set; }

    public bool IsApprovingAuthority { get; set; }
}
