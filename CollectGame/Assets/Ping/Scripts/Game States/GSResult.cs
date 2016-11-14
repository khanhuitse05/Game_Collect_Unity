using UnityEngine;
using UnityEngine.UI;

public class GSResult : GSTemplate
{
    static GSResult _instance;
    public static GSResult Instance { get { return _instance; } }

    public Text txtScore;
    public Text txtBest;
    public Text txtTime;
    public Text txtStar;

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
        txtScore.text = "" + Utils.getScore();
        txtBest.text = "BEST    " + GamePreferences.Instance.currentHighScore;
        txtStar.text = "+" + GamePreferences.Instance.setting.currentStar * 10;
        txtTime.text = "+" + GamePreferences.Instance.setting.currentTime;
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
        GameStatesManager.Instance.stateMachine.PushState(GSHome.Instance);
    }
    public void OnClickPlay()
    {
        GameStatesManager.Instance.stateMachine.PushState(GSGamePlay.Instance);
    }
    public void OnClickHome()
    {
        GameStatesManager.Instance.stateMachine.PushState(GSHome.Instance);
    }
    public void OnClickRate() { }
    public void OnClickShare() { }
}