using System;
using System.Collections.Generic;

namespace schedule;

public partial class TblDay
{
    public int Id { get; set; }

    public int? DaySid { get; set; }

    public int? PairId { get; set; }

    public virtual TblPair? Pair { get; set; }
}
