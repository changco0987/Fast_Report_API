using System;
using System.Collections.Generic;

namespace Fast_Report_API.Models;

public partial class PurchaseOrdersTbl
{
    public ulong PurchaseOrderId { get; set; }

    public ulong NoaId { get; set; }

    public ulong CommoditiesId { get; set; }

    public ulong StatusId { get; set; }

    public string PurchaseOrderNumber { get; set; } = null!;

    public DateTime? SupplierDate { get; set; }

    public string? FundCluster { get; set; }

    public decimal FundsAvailable { get; set; }

    public string? OrsBursNumber { get; set; }

    public DateTime? OrsBursDate { get; set; }

    public decimal Amount { get; set; }

    public ulong SignatureAuthorizedOfficialId { get; set; }

    public ulong SignatureChiefAccountantId { get; set; }

    public int? EncodedBy { get; set; }

    public DateTime? EncodedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public DateTime? DateNeeded { get; set; }

    public string? DeliveryTerm { get; set; }

    public string? PaymentTerm { get; set; }

    public string? PlaceOfDelivery { get; set; }

    public virtual CommoditiesTbl Commodities { get; set; } = null!;

    public virtual NoaTbl Noa { get; set; } = null!;

    public virtual UsersTbl SignatureAuthorizedOfficial { get; set; } = null!;

    public virtual UsersTbl SignatureChiefAccountant { get; set; } = null!;

    public virtual StatusTbl Status { get; set; } = null!;
}
