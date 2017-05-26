using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GSTutorial : GSTemplate
{
    static GSTutorial _instance;
    public static GSTutorial Instance { get { return _instance; } }


    public Text txtTime;
    public GameObject[] tutorialObj;
    int step;

    public GameObject pfCreepSpike;
    public GameObject pfCreepRotate;
    CreepSpikeTutorial cSpike;

    protected override void Awake()
    {
        base.Awake();
        _instance = this;
    }
    protected override void init()
    {
        dragDistance = Screen.height * 5 / 100;
    }
    public override void onEnter()
    {
        base.onEnter();
        StartCoroutine(UpdateTime());
        OnStartGame();
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
        StopCoroutine(UpdateTime());
    }
    protected override void onBackKey()
    {
    }
    public void OnDestroy()
    {
        Destroy(guiMain);
        Destroy(gameObject);
    }
    void OnStartGame()
    {
        GamePreferences.Instance.setting.swipe = true;
        GameObject _obj = Utils.Spawn(pfHero);
        hero = _obj.GetComponent<HeroTutorial>();
        Utils.Spawn(GSGamePlay.Instance.explodeSpawn).transform.position = hero.transform.position;
        hero.Init(2);
        time = 0;
        step = -1;
        NextStep();
    }
    void NextStep()
    {
        if (step >= 0)
        {
            tutorialObj[step].SetActive(false);
            Time.timeScale = 1;
        }
        step++;
        if (step >= tutorialObj.Length)
        {
            FinishTutorial();
        }
        else
        {
            state = StateTutorial.Free;
            if (step == (tutorialObj.Length - 1))
            {
                Time.timeScale = 1;
                tutorialObj[step].SetActive(true);
            }
        }
    }
    public void FinishTutorial()
    {
        Destroy(hero.gameObject);
        GamePreferences.Instance.setting.enableTutorial = false;
        GamePreferences.Instance.SaveSetting();
        GameStatesManager.Instance.stateMachine.SwitchState(GSHome.Instance);
        SpawnManager.Instance.Init();
        OnDestroy();
    }

    int _time;
    int time
    {
        get { return _time; }
        set
        {
            _time = value;
            txtTime.text = timeToText(_time);
        }
    }
    IEnumerator UpdateTime()
    {
        while (true)
        {
            time++;
            yield return new WaitForSeconds(1);
        }
    }
    string timeToText(float _time)
    {
        int _min = (int)_time / 60;
        int _sec = (int)_time % 60;
        return "" + _min + ":" + intToText(_sec);
    }
    string intToText(int _value)
    {
        return _value < 10 ? "0" + _value : "" + _value;
    }


    public GameObject pfHero;
    HeroTutorial hero;
    bool wait;
    Vector3 fpos;
    Vector3 lpos;
    float dragDistance;
    StateTutorial state;

    void Update()
    {
        UpdateMove();
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.UpArrow))
            JumeUp();
        if (Input.GetKeyDown(KeyCode.DownArrow))
            JumeDown();
        if (Input.GetKeyDown(KeyCode.RightArrow))
            TurnRight();
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            TurnLeft();
#else
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                wait = true;
                fpos = touch.position;
                lpos = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved && wait)
            {
                lpos = touch.position;
                touchPhaseMove();
            }
        }
#endif

    }
    void touchPhaseMove()
    {
        if (Mathf.Abs(lpos.x - fpos.x) > dragDistance || Mathf.Abs(lpos.y - fpos.y) > dragDistance)
        {
            if (Mathf.Abs(lpos.x - fpos.x) > Mathf.Abs(lpos.y - fpos.y))
            {
                if ((lpos.x > fpos.x))
                {
                    wait = false;
                    TurnRight();
                }
                else
                {
                    wait = false;
                    TurnLeft();
                }
            }
            else
            {
                if (lpos.y > fpos.y)
                {
                    wait = false;
                    JumeUp();
                }
                else
                {
                    wait = false;
                    JumeDown();
                }
            }
        }

    }
    void UpdateMove()
    {
        if (state == StateTutorial.Move)
        {
            if (hero.velocity.x > 0 && hero.transform.position.x > (Pos.center.x - 0.5f))
            {
                StartStep();
            }
            else if (hero.velocity.x < 0 && hero.transform.position.x < (Pos.center.x + 0.5f))
            {
                StartStep();
            }
        }
    }
    void JumeUp()
    {
        if (state == StateTutorial.Free)
        {
            hero.JumeUp();
        }
        if (step == 2 && state == StateTutorial.Pause)
        {
            NextStep();
            hero.JumeUp();
        }
    }
    void JumeDown()
    {
        if (state == StateTutorial.Free)
        {
            hero.JumeDown();
        }
        if (step == 3 && state == StateTutorial.Pause)
        {
            NextStep();
            hero.JumeDown();
        }
    }
    void TurnLeft()
    {
        if (state == StateTutorial.Free)
        {
            hero.TurnLeft();
        }
        if (step == 0 && state == StateTutorial.Pause)
        {
            NextStep();
            hero.TurnLeft();
        }
    }
    void TurnRight()
    {
        if (state == StateTutorial.Free)
        {
            hero.TurnRight();
        }
        if (step == 1 && state == StateTutorial.Pause)
        {
            NextStep();
            hero.TurnRight();
        }
    }
    public void HeroOverFarm()
    {
        if (state == StateTutorial.Free)
        {
            if (cSpike != null)
            {
                cSpike.OnHide();
            }
            PrepareStep();
        }
    }
    void PrepareStep()
    {
        switch (step)
        {
            case 0:
                if (hero.velocity.x > 0)
                {
                    SpawnSpike();
                    state = StateTutorial.Move;
                }
                break;
            case 1:
                if (hero.velocity.x < 0)
                {
                    SpawnSpike();
                    state = StateTutorial.Move;
                }
                break;
            case 2:
                    SpawnRotate();
                    state = StateTutorial.Move;
                break;
            case 3:
                if (hero.lane > 0)
                {
                    SpawnRotate();
                    state = StateTutorial.Move;
                }
                break;
            default:
                break;
        }
    }
    void StartStep()
    {
        Time.timeScale = 0;
        tutorialObj[step].SetActive(true);
        state = StateTutorial.Pause;
    }
    
    void SpawnSpike()
    {
        GameObject _obj = Utils.Spawn(pfCreepSpike);
        cSpike = _obj.GetComponent<CreepSpikeTutorial>();
        cSpike.Init(hero.lane);
    }
    void SpawnRotate()
    {
        int _pos = hero.velocity.x > 0 ? 1 : 0;
        GameObject _obj = Utils.Spawn(pfCreepRotate);
        CreepRotateTutorial _creep = _obj.GetComponent<CreepRotateTutorial>();
        _creep.Init(hero.lane, _pos);
    }
}
enum StateTutorial
{
    Free,
    Move,
    Pause,
}
