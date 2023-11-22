using System;
using System.Collections.Generic;

namespace Fast_Report_API.Models;

public partial class UomTbl
{
    public ulong UomId { get; set; }

    public string? UomName { get; set; }

    public string? UomAcronym { get; set; }

    public ulong StatusId { get; set; }

    public virtual ICollection<NoaDetailsTbl> NoaDetailsTbls { get; set; } = new List<NoaDetailsTbl>();

    public virtual StatusTbl Status { get; set; } = null!;
}
