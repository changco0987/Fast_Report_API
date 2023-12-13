using System;
using System.Collections.Generic;

namespace Fast_Report_API.Models;

public partial class NoaTbl
{
    public ulong NoaId { get; set; }

    public string? NoaContractId { get; set; }

    public ulong SupplierId { get; set; }

    public string? NoaTitle { get; set; }

    public string PurTbl { get; set; } = null!;

    public DateTime? ModeOfPrecurementDate { get; set; }

    public ulong DepartmentOfficeId { get; set; }

    public ulong ModeOfPrecurementId { get; set; }

    public ulong StatusId { get; set; }

    public int? EncodedBy { get; set; }

    public DateTime? EncodedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public decimal GrandTotal { get; set; }

    public string? GrandTotalAmountInWords { get; set; }

    public bool PerfSec { get; set; }

    public decimal? PerfSec30 { get; set; }

    public decimal? PerfSec5 { get; set; }

    public ulong PerformanceId { get; set; }

    public ulong AppAuthUserId { get; set; }

    public DateTime? DateNeeded { get; set; }

    public string? Type { get; set; }

    public DateTime? DateBid { get; set; }

    public DateTime? DateAwarded { get; set; }

    public string? CommitteeType { get; set; }

    public virtual UsersTbl AppAuthUser { get; set; } = null!;

    public virtual ICollection<AttachmentTbl> AttachmentTbls { get; set; } = new List<AttachmentTbl>();

    public virtual DepartmentsTbl DepartmentOffice { get; set; } = null!;

    public virtual ModeOfProcurementTbl ModeOfPrecurement { get; set; } = null!;

    public virtual ICollection<NoaDetailsTbl> NoaDetailsTbls { get; set; } = new List<NoaDetailsTbl>();

    public virtual PerformanceBondTbl Performance { get; set; } = null!;

    public virtual ICollection<PurchaseOrdersTbl> PurchaseOrdersTbls { get; set; } = new List<PurchaseOrdersTbl>();

    public virtual StatusTbl Status { get; set; } = null!;

    public virtual SuppliersTbl Supplier { get; set; } = null!;

    public virtual ICollection<TermsAndConditionTbl> TermsAndConditionTbls { get; set; } = new List<TermsAndConditionTbl>();
}
