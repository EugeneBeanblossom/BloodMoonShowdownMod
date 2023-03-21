using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodMoonShowdownMod.Harmony
{
    [HarmonyPatch(typeof(EntityPlayerLocal))]
    [HarmonyPatch("OnHUD")]
    public class OpenShowdownWindow
    {
        private static bool Prefix(EntityPlayerLocal __instance)
        {
            if (!__instance.PlayerUI.windowManager.IsModalWindowOpen() && !__instance.PlayerUI.windowManager.IsWindowOpen("showdown"))
                __instance.PlayerUI.windowManager.Open("showdown", false);

            return true;
        }
    }
}
