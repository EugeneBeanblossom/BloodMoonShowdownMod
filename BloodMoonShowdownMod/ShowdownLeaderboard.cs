using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodMoonShowdownMod
{
    public class ShowdownLeaderboard
    {
        private ShowdownLeaderboard()
        {
            Players = new Dictionary<int, LeaderboardEntry>();
        }

        private static ShowdownLeaderboard _instance;
        public static ShowdownLeaderboard Instance { 
            get
            {
                if(_instance == null)
                {
                    _instance = new ShowdownLeaderboard();
                }
                return _instance;
            }
        }

        public Dictionary<int, LeaderboardEntry> Players { get; set; }

        public void AddPlayerScore(int entityId, int kills)
        {
            LeaderboardEntry entry = null;
            if (Players.ContainsKey(entityId))
            {
                entry = Players[entityId];
            }
            else
            {
                var player = GameManager.Instance.World.Players.dict[entityId];
                if (player != null)
                {
                    entry = new LeaderboardEntry
                    {
                        EntityId = player.entityId,
                        Kills = 0,
                        SteamId = player.EntityName
                    };
                }
                else
                {
                    Log.Error($"Unable to find player with id {entityId}");
                    return;
                }
            }
            if(entry == null)
            {
                Log.Error($"Found empty leaderboard entry for entity id {entityId}");
                return;
            }

            entry.Kills += kills;
        }

        public void ResetAndInit()
        {
            Log.Out("*** Starting Blood Moon Showdown ***");
            Players.Clear();
            foreach(var player in GameManager.Instance.World.Players.list)
            {
                if (!Players.ContainsKey(player.entityId))
                {
                    Players.Add(player.entityId, new LeaderboardEntry
                    {
                        EntityId = player.entityId,
                        Kills = 0,
                        SteamId = player.EntityName
                    });
                }
            }
        }

        public void End()
        {
            Log.Out("*** Ending Blood Moon Showdown ***");
        }

        public void UpdateFromServer(LeaderboardEntry entry)
        {
            if (entry != null && Players.ContainsKey(entry.EntityId))
            {
                Players[entry.EntityId].Kills = entry.Kills;
                Players[entry.EntityId].SteamId = entry.SteamId;
            }
            else
            {
                Players.Add(entry.EntityId, new LeaderboardEntry
                {
                    EntityId = entry.EntityId,
                    Kills = entry.Kills,
                    SteamId = entry.SteamId
                });
            }
        }
    }
}
