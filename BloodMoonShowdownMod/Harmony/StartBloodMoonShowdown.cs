using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodMoonShowdownMod.Harmony
{
    [HarmonyPatch(typeof(AIDirectorBloodMoonComponent))]
    [HarmonyPatch("StartBloodMoon")]
    public class StartBloodMoonShowdown
    {
        private static bool Prefix(AIDirectorBloodMoonComponent __instance)
        {
            ShowdownLeaderboard.Instance.ResetAndInit();

            return true;
        }
    }
}
