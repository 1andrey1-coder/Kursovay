using System;
using System.Collections.Generic;

namespace schedule;

public partial class TblSemester
{
    public int Id { get; set; }

    public int? SemesterNumber { get; set; }

    public int? SemesterWeek { get; set; }

    public int? SpecialityId { get; set; }
}
