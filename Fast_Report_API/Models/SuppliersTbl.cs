using System;
using System.Collections.Generic;

namespace Fast_Report_API.Models;

public partial class SuppliersTbl
{
    public ulong SupplierId { get; set; }

    public string? SupplierName { get; set; }

    public string? ContactPerson { get; set; }

    public string? Contact { get; set; }

    public string? Email { get; set; }

    public string? Position { get; set; }

    public string? Tin { get; set; }

    public int? EncodedBy { get; set; }

    public DateTime? EncodedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? DeliveryAddress { get; set; }

    public string? FaxNumber { get; set; }

    public string? AttentionTitle { get; set; }

    public virtual ICollection<NoaTbl> NoaTbls { get; set; } = new List<NoaTbl>();
}
