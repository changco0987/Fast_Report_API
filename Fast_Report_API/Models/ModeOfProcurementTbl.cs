using System;
using System.Collections.Generic;

namespace Fast_Report_API.Models;

public partial class ModeOfProcurementTbl
{
    public ulong ModeId { get; set; }

    public string? ModeName { get; set; }

    public string? ModeDescription { get; set; }

    public virtual ICollection<NoaTbl> NoaTbls { get; set; } = new List<NoaTbl>();
}
