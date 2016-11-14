using UnityEngine;
using System;

public class GamePreferences : MonoBehaviour
{

    static GamePreferences _instance;
    public static GamePreferences Instance { get { return _instance; } }
    void Awake()
    {
        _instance = this;
        LoadSetting();
    }
    /// <summary>
    /// Setting
    /// </summary>
    public Setting setting { get; set; }
    public Setting LoadSetting()
    {
        setting = SaveGameManager.loadData<Setting>(GameTags.settingDataKey);
        if (setting == null)
        {
            setting = new Setting();
            SaveSetting();
        }
        return setting;
    }
    public void SaveSetting()
    {
        SaveGameManager.saveData<Setting>(GameTags.settingDataKey, setting);
    }
    public int currentHighScore { get { return setting.highScore[setting.currentDifficulty]; } }

}

public class Setting
{
    public string version;
    public bool sound;
    public bool enableTutorial;
    public int rate;
    public int[] highScore;
    public int currentTime;
    public int currentStar;
    public int currentDifficulty;
    public int coin;

    public void updateScore(int _star, int _time)
    {
        currentTime = _time;
        currentStar = _star;
        int _newScore = Utils.getScore(_star, _time);
        if (_newScore > highScore[currentDifficulty])
        {
            highScore[currentDifficulty] = _newScore;
        }
    }
    public void updateCoin(int _value)
    {
        coin = Mathf.Clamp(coin + _value, 0, Int32.MaxValue);
    }
    public Setting()
    {
        version = GameConstants.gameVersion;
        sound = true;
        enableTutorial = true;
        rate = 0;
        coin = 0;
        currentStar = 0;
        currentTime = 0;
        highScore = new int[2];
        for (int i = 0; i < highScore.Length; i++)
        {
            highScore[i] = 0;
        }
    }
}