using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schedule
{
    public class DB
    {
        internal static object TblReplacements;
        private static ScheduleDbContext Instance;
        public static ScheduleDbContext GetInstance()
        {
            if (Instance == null)
                Instance = new();
            return Instance;
        }
    }
}
