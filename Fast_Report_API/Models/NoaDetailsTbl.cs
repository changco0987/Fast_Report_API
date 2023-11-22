using System;
using System.Collections.Generic;

namespace Fast_Report_API.Models;

public partial class NoaDetailsTbl
{
    public ulong NoaDetailsId { get; set; }

    public ulong NoaId { get; set; }

    public decimal Quantity { get; set; }

    public int ItemNumber { get; set; }

    public ulong UomId { get; set; }

    public decimal UnitCost { get; set; }

    public decimal TotalCost { get; set; }

    public string? Description { get; set; }

    public string? StockPropertyNumber { get; set; }

    public virtual NoaTbl Noa { get; set; } = null!;

    public virtual UomTbl Uom { get; set; } = null!;
}
