using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class XUiC_ShowdownPlayer : XUiController
{
    /// <summary>
    /// -2 if not set to a player
    /// </summary>
    public int EntityId { get; set; }
    public XUiV_Label KillsText { get; set; }
    public XUiV_Label RankText { get; set; }
    public XUiV_Label SteamIdText { get; set; }
    public XUiV_Sprite Background { get; set; }

    private static Color _defaultColor = StringParsers.ParseColor32("64,64,64");
    private static Color _currentPlayerColor = StringParsers.ParseColor32("5,64,5");

    public override void Init()
    {
        base.Init();
        Background = (XUiV_Sprite)GetChildById("background").ViewComponent;
        KillsText = (XUiV_Label)GetChildById("killsText").ViewComponent;
        RankText = (XUiV_Label)GetChildById("rankText").ViewComponent;
        SteamIdText = (XUiV_Label)GetChildById("steamIdText").ViewComponent;
    }

    public override void Update(float _dt)
    {
        base.Update(_dt);
        // Highlight the current player on the leaderboard
        Background.Color = (EntityId == GameManager.Instance.World.GetPrimaryPlayerId() ? _currentPlayerColor : _defaultColor);
    }
}
