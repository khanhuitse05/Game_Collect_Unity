using UnityEngine;
using UnityEngine.UI;

public class GSHome : GSTemplate
{
    static GSHome _instance;
    public static GSHome Instance { get { return _instance; } }

    public Image normal;
    public Image hard;
    public Text txtNormal;
    public Text txtHard;

    protected override void Awake()
    {
        base.Awake();
        _instance = this;
    }
    protected override void init()
    {
    }
    public override void onEnter()
    {
        base.onEnter();
        SetDifficulty();
        txtNormal.text = "" + GamePreferences.Instance.setting.highScore[0];
        txtHard.text = "" + GamePreferences.Instance.setting.highScore[1];
    }
    public override void onResume()
    {
        base.onResume();
    }
    public override void onSuspend()
    {
        base.onSuspend();
    }
    public override void onExit()
    {
        base.onExit();
    }
    protected override void onBackKey()
    {
    }
    public void OnClickPlay()
    {
        GameStatesManager.Instance.stateMachine.PushState(GSGamePlay.Instance);
    }
    public void OnClickNormal()
    {
        GamePreferences.Instance.setting.currentDifficulty = 0;
        SetDifficulty();
    }
    public void OnClickHard()
    {
        GamePreferences.Instance.setting.currentDifficulty = 1;
        SetDifficulty();
    }
    void SetDifficulty()
    {
        int _diff = GamePreferences.Instance.setting.currentDifficulty;
        if (_diff == 0)
        {
            normal.color = new Color(1f, 1f, 1f, 1f);
            hard.color = new Color(1f, 1f, 1f, 0.005f);
        }
        else
        {
            normal.color = new Color(1f, 1f, 1f, 0.005f);
            hard.color = new Color(1f, 1f, 1f, 1f);
        }

    }
    public void OnClickInfo() { }
    public void OnClickRate() { }
    public void OnClickShare() { }
    public void OnClickShop() { }
}