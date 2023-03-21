using BloodMoonShowdownMod.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BloodMoonShowdownMod
{
    /*
     * Performance and Feature Considerations:
     * - Currently the score is updated on the event "EntityKilled" which is fired for each kill - perhaps this could be batched?
     * - The entire leaderboard is sent to each player for each kill because I was lazy. This updates the player list and the scores simultaneously.
     *   The alternative is to update the list of leaderboard players whenever someone joins (and possibly leaves) the server and then seperately 
     *   update the individual user's score when they kill something.
     * - Currently the scores update regardless of what's killed.  This includes non-hostile animals (e.g. chickens). We could filter the type.
     * - We could play sounds for certain events such as when you move into first or win at the end.
     * - We could track and show detailed stats at the end.
     */
    public class BloodMoonShowdown : IModApi
    {
        public void InitMod(Mod _modInstance)
        {
            Log.Out(" *** Loading BloodMoonShowdown *** ");

            var harmony = new HarmonyLib.Harmony(GetType().ToString());
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            ModEvents.EntityKilled.RegisterHandler(HandleEntityKilled);
        }

        private void HandleEntityKilled(Entity victim, Entity killer)
        {
            if (SingletonMonoBehaviour<ConnectionManager>.Instance.IsServer)
            {
                if (victim is EntityAlive eaVictim && killer is EntityPlayer player)
                {
                    if (GameManager.Instance.World.aiDirector.BloodMoonComponent.BloodMoonActive)
                    {
                        ShowdownLeaderboard.Instance.AddPlayerScore(killer.entityId, 1);
                        
                        SingletonMonoBehaviour<ConnectionManager>.Instance.SendPackage(NetPackageManager.GetPackage<NetPackageLeaderboard>().Setup(), true);
                    }
                }
            }
        }
    }
}
