using System;
using System.Collections.Generic;

namespace schedule;

public partial class TblCourse
{
    public int Id { get; set; }

    public int? Course { get; set; }

    public virtual ICollection<TblGroup> TblGroups { get; } = new List<TblGroup>();

    public virtual ICollection<TblSemestr> TblSemestrs { get; } = new List<TblSemestr>();
}
