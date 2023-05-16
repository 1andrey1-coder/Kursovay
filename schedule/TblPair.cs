using System;
using System.Collections.Generic;

namespace schedule;

public partial class TblPair
{
    public int Id { get; set; }

    public int? Pairid { get; set; }

    public virtual ICollection<TblDay> TblDays { get; } = new List<TblDay>();
}
