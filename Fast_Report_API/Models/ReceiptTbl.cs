using System;
using System.Collections.Generic;

namespace Fast_Report_API.Models;

public partial class ReceiptTbl
{
    public ulong ReceiptId { get; set; }

    public ulong NoaId { get; set; }

    public string? ReceiptNumber { get; set; }

    public int? EncodedBy { get; set; }

    public DateTime? EncodedDate { get; set; }

    public virtual NoaTbl Noa { get; set; } = null!;
}
