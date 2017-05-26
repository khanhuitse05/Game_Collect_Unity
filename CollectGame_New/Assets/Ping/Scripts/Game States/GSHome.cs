using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GSHome : GSTemplateZoom
{
    static GSHome _instance;
    public static GSHome Instance { get { return _instance; } }

    public SpriteRenderer bg;
    public GameObject normalChoose;
    public GameObject normalPlay;
    public GameObject hardChoose;
    public GameObject hardPlay;
    public Text txtNormal;
    public Text txtHard;
    public ShopContent shop;
    public GameObject btnRank;
    public GridLayoutGroup grid;

    protected override void Awake()
    {
        base.Awake();
        _instance = this;
    }
    protected override void init()
    {
#if !UNITY_ANDROID
        btnRank.SetActive(false);
        grid.spacing = new Vector2(60,0);
#endif
        shop.Init();
    }

    protected override void ShowContent()
    {
        //shop.gameObject.SetActive(true);
        shop.ChangeIndex();
    }

    protected override void HideContent()
    {
        //shop.gameObject.SetActive(false);
    }
    public override void onEnter()
    {
        base.onEnter();
        txtNormal.text = "" + GamePreferences.Instance.setting.highScore[0];
        txtHard.text = "" + GamePreferences.Instance.setting.highScore[1];
        SetDifficulty();
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
        bg.gameObject.SetActive(false);
    }
    protected override IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(timeOut);
        bg.gameObject.SetActive(true);
        guiMain.SetActive(true);
        canvasGroup.interactable = false;
        guiMain.transform.localScale = scaleEffect;
        iTween.ScaleTo(guiMain, Vector3.one, timeIn);
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.fixedDeltaTime / timeIn;
            yield return null;
        }
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        ShowContent();
    }
    protected override void onBackKey()
    {
    }    
    public void OnClickPlay()
    {
        GameConstants.PlaySoundClick();
        shop.GetIDPlay();
        GameStatesManager.Instance.stateMachine.SwitchState(GSGamePlay.Instance);
    }
    public void OnClickNormal()
    {
        GameConstants.PlaySoundClick();
        GamePreferences.Instance.setting.currentDifficulty = 0;
        SetDifficulty();
    }
    public void OnClickHard()
    {
        GameConstants.PlaySoundClick();
        GamePreferences.Instance.setting.currentDifficulty = 1;
        SetDifficulty();
    }
    void SetDifficulty()
    {
        int _diff = GamePreferences.Instance.setting.currentDifficulty;
        if (_diff == 0)
        {
            normalChoose.SetActive(false);
            normalPlay.SetActive(true);
            hardChoose.SetActive(true);
            hardPlay.SetActive(false);
        }
        else
        {
            normalChoose.SetActive(true);
            normalPlay.SetActive(false);
            hardChoose.SetActive(false);
            hardPlay.SetActive(true);
        }
        shop.SetDifficulty();
    }
    public void OnClickInfo()
    {
        GameConstants.PlaySoundClick();
    }
    public void OnClickRate()
    {
        GameConstants.PlaySoundClick();
        Application.OpenURL(Utils.GetLinkStore());
    }
    public void OnClickShare()
    {
        GameConstants.PlaySoundClick();
    }
    public void OnClickRank()
    {
        GameConstants.PlaySoundClick();
    }
    public void OnClickSetting()
    {
        GameConstants.PlaySoundClick();
        PopupManager.Instance.InitSeting();
    }
}