using System;
using System.Collections.Generic;

namespace Fast_Report_API.Models;

public partial class CommoditiesTbl
{
    public ulong CommoditiesId { get; set; }

    public string? Unit { get; set; }

    public string? PersonInCharge { get; set; }

    public string Commodities { get; set; } = null!;

    public int? EncodedBy { get; set; }

    public DateTime? EncodedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? Prefix { get; set; }

    public virtual ICollection<PurchaseOrdersTbl> PurchaseOrdersTbls { get; set; } = new List<PurchaseOrdersTbl>();
}
