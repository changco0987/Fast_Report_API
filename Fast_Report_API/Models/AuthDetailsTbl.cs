using System;
using System.Collections.Generic;

namespace Fast_Report_API.Models;

public partial class AuthDetailsTbl
{
    public ulong AuthDetailId { get; set; }

    public ulong AuthId { get; set; }

    public ulong AccessId { get; set; }

    public virtual AccessTbl Access { get; set; } = null!;

    public virtual AuthorizationTbl Auth { get; set; } = null!;
}
