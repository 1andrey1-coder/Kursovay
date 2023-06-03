using System;
using System.Collections.Generic;

namespace schedule;

public partial class TblName
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<TblObpred> TblObpreds { get; } = new List<TblObpred>();
}
