using System;
using System.Collections.Generic;

namespace Fast_Report_API.Models;

public partial class AuthorizationTbl
{
    public ulong Id { get; set; }

    public string? AuthorizationName { get; set; }

    public virtual ICollection<AuthDetailsTbl> AuthDetailsTbls { get; set; } = new List<AuthDetailsTbl>();

    public virtual ICollection<DepartmentsTbl> DepartmentsTbls { get; set; } = new List<DepartmentsTbl>();

    public virtual ICollection<UsersTbl> UsersTbls { get; set; } = new List<UsersTbl>();
}
