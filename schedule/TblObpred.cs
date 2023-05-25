using System;
using System.Collections.Generic;

namespace schedule;

public partial class TblObpred
{
    public int Id { get; set; }

    public string? Predmet { get; set; }

    public int? CourseId { get; set; }

    public virtual TblCourse? Course { get; set; }
}
