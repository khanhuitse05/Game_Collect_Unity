using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GSSplash : GSTemplateZoom
{
    static GSSplash _instance;
    public static GSSplash Instance { get { return _instance; } }
    public float timeLive = 1.5f;
    public Image logo;
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
        //Color from = new Color(logo.color.r, logo.color.g, logo.color.b, 0f);
        //Color to = logo.color;
        //logo.color = from;
        //iTween.ValueTo(logo.gameObject, iTween.Hash("from", logo.color,
        //    "to", to,
        //    "ignoretimescale", false,
        //    "time", 1f,
        //    "easetype", iTween.EaseType.linear,
        //    "onupdate", "OnStarseedImageUpdate",
        //    "onupdatetarget", gameObject));
        StartCoroutine(FinishState());
    }

    //private void OnStarseedImageUpdate(Color currentColor)
    //{
    //    logo.color = currentColor;
    //}
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
    IEnumerator FinishState()
    {
        yield return new WaitForSeconds(timeLive);
        if (GamePreferences.Instance.setting.enableTutorial)
        {
            GameStatesManager.Instance.stateMachine.SwitchState(GSTutorial.Instance);
            OnDestroy();
        }
        else
        {
            GSTutorial.Instance.OnDestroy();
            GameStatesManager.Instance.stateMachine.SwitchState(GSHome.Instance);
            OnDestroy();
        }

    }
    void OnDestroy()
    {
        Destroy(guiMain);
        Destroy(gameObject);
    }
    protected override IEnumerator FadeIn()
    {
        guiMain.SetActive(true);
        yield return null;
    }
}

