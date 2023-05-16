using System;
using System.Collections.Generic;

namespace schedule;

public partial class TblWeekday
{
    public int Id { get; set; }

    public string? Day { get; set; }

    public virtual ICollection<TblReplacement> TblReplacements { get; } = new List<TblReplacement>();
}
