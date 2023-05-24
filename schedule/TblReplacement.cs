using System;
using System.Collections.Generic;

namespace schedule;

public partial class TblReplacement
{
    public int Id { get; set; }

    public int? Group { get; set; }

    public int? Pair { get; set; }

    public int? Cabinet { get; set; }

    public string? Name { get; set; }

    public string? Predmet { get; set; }

    public int? WeekdaysId { get; set; }

    public DateTime Date { get; set; }

    public virtual TblWeekday? Weekdays { get; set; }
}
