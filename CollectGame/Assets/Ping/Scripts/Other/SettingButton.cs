using UnityEngine;
using System.Collections;

public class SettingButton : MonoBehaviour {

    public GameObject audioOn;
    public GameObject audioOff;
    void OnEnable()
    {
        Utils.setActive(audioOn, GamePreferences.Instance.setting.sound);
        Utils.setActive(audioOff, !GamePreferences.Instance.setting.sound);
    }
    public void onBtnSettingsClick()
    {
        float value = 0;
        if (GamePreferences.Instance.setting.sound)
        {
            value = 0f;
            GamePreferences.Instance.setting.sound = false;
            Utils.setActive(audioOn, false);
            Utils.setActive(audioOff, true);
        }
        else
        {
            value = 1.0f;
            GamePreferences.Instance.setting.sound = true;
            Utils.setActive(audioOn, true);
            Utils.setActive(audioOff, false);
        }
        AudioManager.SetSFXVolume(value);
        GamePreferences.Instance.SaveSetting();
    }
}
