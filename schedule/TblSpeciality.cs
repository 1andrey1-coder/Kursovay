using System;
using System.Collections.Generic;

namespace schedule;

public partial class TblSpeciality
{
    public int SpecialityId { get; set; }

    public string? Speciality { get; set; }

    public virtual ICollection<TblGroup> TblGroups { get; } = new List<TblGroup>();
}
