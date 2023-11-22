using System;
using System.Collections.Generic;

namespace Fast_Report_API.Models;

public partial class SeriesTbl
{
    public ulong SeriesId { get; set; }

    public string? Screen { get; set; }

    public string? Prefix { get; set; }

    public decimal? Number { get; set; }

    public ulong StatusId { get; set; }

    public virtual StatusTbl Status { get; set; } = null!;
}
