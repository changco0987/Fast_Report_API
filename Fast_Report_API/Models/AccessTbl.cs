using System;
using System.Collections.Generic;

namespace Fast_Report_API.Models;

public partial class AccessTbl
{
    public ulong AccessId { get; set; }

    public string? AccessName { get; set; }

    public virtual ICollection<AuthDetailsTbl> AuthDetailsTbls { get; set; } = new List<AuthDetailsTbl>();
}
