using System.Collections;
using UnityEngine;

////////////////////////////////////////////////////////
//Author:
//TODO: a game state sample
////////////////////////////////////////////////////////

public class GSTemplate : IState
{
    public GameObject guiMain;
    public AudioClip musicClip;
    bool isFirst;
    protected float timeIn = 0.2f;
    protected float timeOut = 0.2f;
    protected override void Awake()
    {
        base.Awake();
        guiMain.SetActive(false);
        isFirst = true;
    }
    /// <summary>
    /// One time when start
    /// </summary>
    protected virtual void init()
    {
    }
    protected virtual void onBackKey()
    {
    }
    public override void onSuspend()
    {
        base.onSuspend();
        GameStatesManager.onBackKey = null;
        StartCoroutine(FadeOut());
    }
    public override void onResume()
    {
        base.onResume();
        GameStatesManager.Instance.InputProcessor = guiMain;
        GameStatesManager.onBackKey = onBackKey;
        if (musicClip != null)
        {
            AudioManager.PlayMusic(musicClip, true);
        }
        StartCoroutine(FadeIn());
    }
    public override void onEnter()
    {
        base.onEnter();
        if (isFirst)
        {
            isFirst = false;
            init();
        }
        onResume();
    }
    public override void onExit()
    {
        base.onExit();
        onSuspend();
    }
    protected virtual IEnumerator FadeIn()
    {
        guiMain.SetActive(true);
        yield return null;
    }
    protected virtual IEnumerator FadeOut()
    {
        guiMain.SetActive(false);
        yield return null;
    }
}
