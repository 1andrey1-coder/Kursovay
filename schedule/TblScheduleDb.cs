using System;
using System.Collections.Generic;

namespace schedule;

public partial class TblScheduleDb
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Predmet { get; set; }

    public double? Cabinet { get; set; }

    public string? Day { get; set; }

    public int? Pair { get; set; }

    public int? Groupid { get; set; }

    public virtual TblGroup? Group { get; set; }
}
