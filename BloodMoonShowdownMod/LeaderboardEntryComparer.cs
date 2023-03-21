using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodMoonShowdownMod
{
    public class LeaderboardEntryComparer : IComparer<LeaderboardEntry>
    {
        public int Compare(LeaderboardEntry x, LeaderboardEntry y)
        {
            if (x == null)
                return 1;
            if(y == null) 
                return -1;
            if (x.Kills == y.Kills)
                return 0;
            if (x.Kills > y.Kills)
                return -1;
            return 1;
        }
    }
}
