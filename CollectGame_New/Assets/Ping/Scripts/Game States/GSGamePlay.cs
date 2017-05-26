using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GSGamePlay : GSTemplateZoom
{
    static GSGamePlay _instance;
    public static GSGamePlay Instance { get { return _instance; } }

    public float timeShield = 2;
    public Control[] pfControl;
    Control hero;
    public Text txtStar;
    public Text txtTime;
    public GameObject[] pfFood;
    public GameObject pfShield;
    public float intervalShield;
    public Transform[] lanes;
    public GameObject controlObj;
    List<Food> listFood;

    public int countPlay { get; set; }
    public int typeDead { get; set; }
    PlayState state;
    int _time;
    int _star;
    int time {
        get { return _time; }
        set
        {
            _time = value;
            txtTime.text = timeToText(_time);
        }
    }
    int star
    {
        get { return _star; }
        set
        {
            _star = value;
            txtStar.text = "" + _star;
        }
    }
    public bool isPause { get { return state == PlayState.pause; } }
    public bool isPlay { get { return state == PlayState.play; } }
    public bool isOver { get { return state == PlayState.over; } }

    public GameObject explodeDie;
    public GameObject explodeSpawn;
    public GameObject guiPause;
    public GameObject btnPause;

    protected override void Awake()
    {
        base.Awake();
        _instance = this;
    }
    protected override void init()
    {
        countPlay = 0;
        listFood = new List<Food>();
        StartCoroutine(UpdateTime());
        StartCoroutine(ShieldRoutine());
    }
    public override void onEnter()
    {
        base.onEnter();
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
    }

    protected override void onBackKey()
    {
        if (isPause)
        {
            OnResumeGame();
        }
        else
        {
            OnPauseGame();
        }
    }
    void OnStartGame()
    {
        state = PlayState.play;
        countPlay++;
        typeDead = -1;
        btnPause.SetActive(true);
        btnPause.transform.localScale = Vector3.one;
        int _index = GamePreferences.Instance.setting.currentDifficulty;
        GameObject _obj = Utils.Spawn(pfControl[_index].gameObject);
        hero = _obj.GetComponent<Control>();
        OnResumeGame();
        time = 0;
        star = 0;
        SpawnFood();
        SpawnManager.Instance.StartGame();
    }
    public void OnPauseGame()
    {
        state = PlayState.pause;
        controlObj.SetActive(false);
        StartCoroutine(OnPauseRoutine());
    }
    float _timePauseDelay = 0.2f;
    IEnumerator OnPauseRoutine()
    {
        guiPause.SetActive(true);
        iTween.ScaleTo(btnPause, Vector3.zero, _timePauseDelay);
        guiPause.transform.localScale = scaleEffect;
        iTween.ScaleTo(guiPause, Vector3.one, _timePauseDelay);
        yield return new WaitForSeconds(_timePauseDelay);
        Time.timeScale = 0;
    }
    public void OnResumeGame()
    {
        state = PlayState.play;
        controlObj.SetActive(!GamePreferences.Instance.setting.swipe);
        StartCoroutine(OnResumeRoutine());
    }
    IEnumerator OnResumeRoutine()
    {
        Time.timeScale = 1;
        iTween.ScaleTo(btnPause, Vector3.one, _timePauseDelay);
        iTween.ScaleTo(guiPause, scaleEffect, _timePauseDelay);
        yield return new WaitForSeconds(_timePauseDelay);
        guiPause.SetActive(false);
    }
    public void OnOverGame(float _delay = 0, int _type = -1)
    {
        if (!isOver)
        {
            typeDead = _type;
            OnClearGame();
            Invoke("FinishGame", _delay);
        }
    }
    void OnClearGame()
    {
        state = PlayState.over;
        btnPause.SetActive(false);
        Time.timeScale = 1;
        for (int i = 0; i < listFood.Count; i++)
        {
            ObjectPoolManager.Unspawn(listFood[i].gameObject);
        }
        listFood = new List<Food>();
        SpawnManager.Instance.GameOver();
        GamePreferences.Instance.setting.updateScore(star, time);
        GamePreferences.Instance.setting.updateCoin(star);
        GamePreferences.Instance.SaveSetting();
    }
    void FinishGame()
    {
        if (hero != null)
        {
            Destroy(hero.gameObject);
            hero = null;
        }
        GameStatesManager.Instance.stateMachine.SwitchState(GSResult.Instance);
    }
    void Update()
    {

    }
    IEnumerator UpdateTime()
    {
        while (true)
        {
            if (isPlay)
            {
                time++;
            }
            yield return new WaitForSeconds(1);
        }
    }
    IEnumerator ShieldRoutine()
    {
        while (true)
        {
            if (isPlay)
            {
                if (Random.Range(0, 2) == 0)
                {
                    SpawnShield();
                }
            }
            yield return new WaitForSeconds(intervalShield);
        }
    }
    public void CollectFood(Food _item)
    {
        star++;
        listFood.Remove(_item);
        ObjectPoolManager.Unspawn(_item.gameObject);
        SpawnFood();
    }

    GameObject _objSpawn;
    Food _foodSpawn;
    Vector3 _posSpawn = Vector3.zero;
    int _indexFood;
    void SpawnFood()
    {
        _indexFood = Random.Range(0, pfFood.Length);
        _posSpawn.x = Random.Range(Pos.spawnLeft, Pos.spawnRight);
        _posSpawn.y = lanes[Random.Range(0, lanes.Length)].position.y;
        _objSpawn = ObjectPoolManager.Spawn(pfFood[_indexFood], _posSpawn, Quaternion.identity);
        _foodSpawn = _objSpawn.GetComponent<Food>();
        listFood.Add(_foodSpawn);
    }
    void SpawnShield()
    {
        Vector3 _pos = new Vector3();
        _pos.x = Random.Range(Pos.spawnLeft, Pos.spawnRight);
        _pos.y = lanes[Random.Range(0, lanes.Length)].position.y;
        ObjectPoolManager.Spawn(pfShield, _pos, Quaternion.identity);
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

    public void OnClickSetting()
    {
        GameConstants.PlaySoundClick();
        PopupManager.Instance.InitSeting();
    }
    public void OnClickPause()
    {
        GameConstants.PlaySoundClick();
        OnPauseGame();
    }
    public void OnClickHome()
    {
        GameConstants.PlaySoundClick();
        OnClearGame();
        if (hero != null)
        {
            Destroy(hero.gameObject);
            hero = null;
        }
        GameStatesManager.Instance.stateMachine.SwitchState(GSHome.Instance);
    }
    public void OnClickPlay()
    {
        GameConstants.PlaySoundClick();
        OnResumeGame();
    }
    public void OnClickLeft()
    {
        if (hero != null)
            hero.TurnLeft();
    }
    public void OnClickRight()
    {
        if (hero != null)
            hero.TurnRight();
    }
    public void OnClickUp()
    {
        if (hero != null)
            hero.JumeUp();
    }
    public void OnClickDown()
    {
        if (hero != null)
            hero.JumeDown();
    }
}

public enum PlayState
{
    none,
    play,
    pause,
    over,
}