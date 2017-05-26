using UnityEngine;
using System.Collections;

public class ControlButton : MonoBehaviour
{

    public GameObject swipeOn;
    public GameObject swipeOff;
    void OnEnable()
    {
        Utils.setActive(swipeOn, GamePreferences.Instance.setting.swipe);
        Utils.setActive(swipeOff, !GamePreferences.Instance.setting.swipe);
    }
    public void onBtnClick()
    {
        GameConstants.PlaySoundClick();
        GamePreferences.Instance.setting.swipe = !GamePreferences.Instance.setting.swipe;
        Utils.setActive(swipeOn, GamePreferences.Instance.setting.swipe);
        Utils.setActive(swipeOff, !GamePreferences.Instance.setting.swipe);
        GamePreferences.Instance.SaveSetting();
    }
}
