using System;
using System.Collections.Generic;

namespace Fast_Report_API.Models;

public partial class DepartmentsTbl
{
    public ulong DepartmentId { get; set; }

    public string? DepartmentName { get; set; }

    public ulong StatusId { get; set; }

    public ulong AuthorizationsId { get; set; }

    public virtual AuthorizationTbl Authorizations { get; set; } = null!;

    public virtual ICollection<NoaTbl> NoaTbls { get; set; } = new List<NoaTbl>();

    public virtual StatusTbl Status { get; set; } = null!;

    public virtual ICollection<UsersTbl> UsersTbls { get; set; } = new List<UsersTbl>();
}
