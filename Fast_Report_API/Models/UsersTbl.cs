using System;
using System.Collections.Generic;

namespace Fast_Report_API.Models;

public partial class UsersTbl
{
    public ulong UsersId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public ulong DepartmentId { get; set; }

    public ulong AuthorizationId { get; set; }

    public int PositionId { get; set; }

    public ulong StatusId { get; set; }

    public int? EncodedBy { get; set; }

    public DateTime? EncodedDate { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<AuditTrailTbl> AuditTrailTbls { get; set; } = new List<AuditTrailTbl>();

    public virtual AuthorizationTbl Authorization { get; set; } = null!;

    public virtual DepartmentsTbl Department { get; set; } = null!;

    public virtual ICollection<NoaTbl> NoaTbls { get; set; } = new List<NoaTbl>();

    public virtual ICollection<PurchaseOrdersTbl> PurchaseOrdersTblSignatureAuthorizedOfficials { get; set; } = new List<PurchaseOrdersTbl>();

    public virtual ICollection<PurchaseOrdersTbl> PurchaseOrdersTblSignatureChiefAccountants { get; set; } = new List<PurchaseOrdersTbl>();

    public virtual StatusTbl Status { get; set; } = null!;
}
