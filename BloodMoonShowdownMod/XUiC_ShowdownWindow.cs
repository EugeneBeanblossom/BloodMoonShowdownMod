using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class XUiC_ShowdownWindow : XUiController
{
    public static string ID = "";
    private int _lastBloodMoonDay = -1,
                _nextBloodMoonDay = -1,
                _dawnHour = -1;
    public override void Init()
    {
        base.Init();
        ID = this.WindowGroup.ID;
        _nextBloodMoonDay = GameStats.GetInt(EnumGameStats.BloodMoonDay);
        (int _, int dawnHour) = GameUtils.CalcDuskDawnHours(GameStats.GetInt(EnumGameStats.DayLightLength));
        _dawnHour = dawnHour;
    }

    public override void Update(float _dt)
    {
        base.Update(_dt);
        var aiDirector = GameManager.Instance.World.GetAIDirector();
        if (aiDirector != null)
        {
            (int Days, int Hours, int _) = GameUtils.WorldTimeToElements(GameManager.Instance.World.worldTime);
            var bloodMoon = GameStats.GetInt(EnumGameStats.BloodMoonDay);
            if(bloodMoon > _nextBloodMoonDay)
            {
                _lastBloodMoonDay = _nextBloodMoonDay;
                _nextBloodMoonDay = bloodMoon;
            }
            // Show only during blood moon and up to 1 hour after.
            ViewComponent.IsVisible = aiDirector.BloodMoonComponent.BloodMoonActive ||
                                      (Days == _lastBloodMoonDay + 1 && Hours < _dawnHour + 1);


        }
    }
}
