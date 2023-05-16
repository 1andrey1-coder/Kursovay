using System;
using System.Collections.Generic;

namespace schedule;

public partial class TblNameid
{
    public int NameId { get; set; }

    public string? NameTitle { get; set; }

    public virtual ICollection<TblScheduleDb> TblScheduleDbs { get; } = new List<TblScheduleDb>();
}
