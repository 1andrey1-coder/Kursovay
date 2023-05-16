using System;
using System.Collections.Generic;

namespace schedule;

public partial class TblSemestr
{
    public int Id { get; set; }

    public int? CourseId { get; set; }

    public int? SemesterNuber { get; set; }

    public int? SemesterWeek { get; set; }

    public virtual TblCourse? Course { get; set; }

    public virtual ICollection<TblGroup> TblGroupSemestrNubers { get; } = new List<TblGroup>();

    public virtual ICollection<TblGroup> TblGroupSemestrWeeks { get; } = new List<TblGroup>();
}
