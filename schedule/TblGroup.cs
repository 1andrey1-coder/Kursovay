using System;
using System.Collections.Generic;

namespace schedule;

public partial class TblGroup
{
    public int GroupId { get; set; }

    public int Group { get; set; }

    public int? SpecialityId { get; set; }

    public int? CourseId { get; set; }

    public int? SemestrNuberId { get; set; }

    public int? SemestrWeekId { get; set; }

    public virtual TblCourse? Course { get; set; }

    public virtual TblSemestr? SemestrNuber { get; set; }

    public virtual TblSemestr? SemestrWeek { get; set; }

    public virtual TblSpeciality? Speciality { get; set; }

    public virtual ICollection<TblObpred> TblObpreds { get; } = new List<TblObpred>();

    public virtual ICollection<TblScheduleDb> TblScheduleDbs { get; } = new List<TblScheduleDb>();
}
