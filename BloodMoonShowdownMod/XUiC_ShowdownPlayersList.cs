using BloodMoonShowdownMod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class XUiC_ShowdownPlayersList : XUiController
{
    private XUiV_Grid _showdownPlayersList;
    private const int MAX_PLAYERS_DISPLAYED = 5; // can bind to rows later

    public override void Init()
    {
        base.Init();
        this._showdownPlayersList = (XUiV_Grid)this.GetChildById("showdownPlayersList").ViewComponent;
        UpdatePlayerList();
    }

    public override void Update(float _dt)
    {
        base.Update(_dt);
        if (GameManager.Instance.World.aiDirector.BloodMoonComponent.BloodMoonActive)
        {
            UpdatePlayerList();
        }
    }

    private void UpdatePlayerList()
    {
        var entryList = new List<LeaderboardEntry>(ShowdownLeaderboard.Instance.Players.Values);

        entryList.Sort(new LeaderboardEntryComparer());
        int rank = 0;
        int lastKills = 0;
        int repeat = 0;
        for(int i = 0; i < Children.Count; i++)
        {
            var showdownPlayer = Children[i] as XUiC_ShowdownPlayer;
            var leaderboardEntry = entryList.Count > i ? entryList[i] : null;
            if(leaderboardEntry != null)
            {
                // Sorted so we should have the highest rank first in the list with kills going in desc order
                if(leaderboardEntry.Kills == lastKills)
                {
                    repeat++;
                }
                else
                {
                    rank += repeat + 1;
                    lastKills = leaderboardEntry.Kills;
                    repeat = 0;
                }
                showdownPlayer.EntityId = leaderboardEntry.EntityId;
                showdownPlayer.KillsText.Text = $"{leaderboardEntry.Kills}";
                showdownPlayer.RankText.Text = $"{rank}";
                showdownPlayer.SteamIdText.Text = $"{leaderboardEntry.SteamId}";
            }
            else
            {
                showdownPlayer.EntityId = -2;
                showdownPlayer.KillsText.Text = "";
                showdownPlayer.RankText.Text = "";
                showdownPlayer.SteamIdText.Text = "";
            }
        }
    }
}
