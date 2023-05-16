using System;
using System.Collections.Generic;

namespace schedule;

public partial class TblPredmetid
{
    public int PredmetId { get; set; }

    public string? PredmetTitle { get; set; }

    public string? Course { get; set; }

    public int? SemesterNumber { get; set; }

    public int? SemesterWeek { get; set; }

    public int? SpecialityId { get; set; }

    public virtual TblSpeciality? Speciality { get; set; }

    public virtual ICollection<TblScheduleDb> TblScheduleDbs { get; } = new List<TblScheduleDb>();
}
