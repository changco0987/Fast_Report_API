using System;
using System.Collections.Generic;

namespace Fast_Report_API.Models;

public partial class StatusTbl
{
    public ulong StatusId { get; set; }

    public string? StatusName { get; set; }

    public string? Module { get; set; }

    public int? EncodedBy { get; set; }

    public DateTime? EncodedDate { get; set; }

    public virtual ICollection<DepartmentsTbl> DepartmentsTbls { get; set; } = new List<DepartmentsTbl>();

    public virtual ICollection<NoaTbl> NoaTbls { get; set; } = new List<NoaTbl>();

    public virtual ICollection<PositionTbl> PositionTbls { get; set; } = new List<PositionTbl>();

    public virtual ICollection<PurchaseOrdersTbl> PurchaseOrdersTbls { get; set; } = new List<PurchaseOrdersTbl>();

    public virtual ICollection<SeriesTbl> SeriesTbls { get; set; } = new List<SeriesTbl>();

    public virtual ICollection<UomTbl> UomTbls { get; set; } = new List<UomTbl>();

    public virtual ICollection<UsersTbl> UsersTbls { get; set; } = new List<UsersTbl>();
}
