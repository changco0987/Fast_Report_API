using System;
using System.Collections.Generic;

namespace Fast_Report_API.Models;

public partial class TermsAndConditionTbl
{
    public ulong TermAndConditionId { get; set; }

    public ulong NoaTblId { get; set; }

    public int? EncodedBy { get; set; }

    public DateTime? EncodedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? Number { get; set; }

    public string? Description { get; set; }

    public virtual NoaTbl NoaTbl { get; set; } = null!;
}
