using System;
using System.Collections.Generic;

namespace Fast_Report_API.Models;

public partial class PerformanceBondTbl
{
    public ulong PerformanceId { get; set; }

    public string? PerformanceBondName { get; set; }

    public virtual ICollection<NoaTbl> NoaTbls { get; set; } = new List<NoaTbl>();
}
