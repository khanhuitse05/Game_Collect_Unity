using UnityEngine;
using UnityEngine.UI;

public class GSResult : GSTemplateZoom
{
    static GSResult _instance;
    public static GSResult Instance { get { return _instance; } }

    public Text txtScore;
    public Text txtBest;
    public Text txtTime;
    public Text txtStar;
    public Image imgDead;
    public Sprite[] spriteDead;

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
        int _score = Utils.getScore();
        int _highScore = GamePreferences.Instance.currentHighScore;
        txtScore.text = "" + _score;
        txtBest.gameObject.SetActive(_score >= _highScore);
        txtStar.text = "+" + GamePreferences.Instance.setting.currentStar * 10;
        txtTime.text = "+" + GamePreferences.Instance.setting.currentTime;
        int _type = GSGamePlay.Instance.typeDead;
        if (_type < 0)
        {
            imgDead.gameObject.SetActive(false);
        }
        else
        {
            imgDead.gameObject.SetActive(true);
            imgDead.sprite = spriteDead[_type];
        }
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
    protected override void ShowContent()
    {
        base.ShowContent();
        if (GSGamePlay.Instance.countPlay == 7 && GamePreferences.Instance.setting.rate < 2)
        {
            GamePreferences.Instance.setting.rate++;
            GamePreferences.Instance.SaveSetting();
            PopupManager.Instance.InitYesNoPopUp("Love <b>Jumping Jumping</b>?\nTell other how you fell.", OnClickRate, null, "RATE", "LATER");
        }
    }
    protected override void onBackKey()
    {
        GameStatesManager.Instance.stateMachine.SwitchState(GSHome.Instance);
    }
    public void OnClickPlay()
    {
        GameConstants.PlaySoundClick();
        GameStatesManager.Instance.stateMachine.SwitchState(GSGamePlay.Instance);
    }
    public void OnClickHome()
    {
        GameConstants.PlaySoundClick();
        GameStatesManager.Instance.stateMachine.SwitchState(GSHome.Instance);
    }

    public void OnClickSetting()
    {
        GameConstants.PlaySoundClick();
        PopupManager.Instance.InitSeting();
    }
    public void OnClickRate()
    {
        GameConstants.PlaySoundClick();
        Application.OpenURL(Utils.GetLinkStore());
    }
    public void OnClickInfo()
    {
        GameConstants.PlaySoundClick();
    }
    public void OnClickRank()
    {
        GameConstants.PlaySoundClick();
    }
    public void OnClickShare()
    {
        GameConstants.PlaySoundClick();
    }
}