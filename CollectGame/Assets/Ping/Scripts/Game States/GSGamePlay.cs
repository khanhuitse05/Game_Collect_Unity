using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GSGamePlay : GSTemplate
{
    static GSGamePlay _instance;
    public static GSGamePlay Instance { get { return _instance; } }

    public Control[] pfHero;
    Control hero;
    public Text txtStar;
    public Text txtTime;
    public GameObject food;
    public Transform[] lanes;
    List<Food> listFood;

    int countPlay;
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
    public bool isPause { get { return state == PlayState.pause; }}
    public bool isPlay { get { return state == PlayState.play; }}
    public bool isOver { get { return state == PlayState.over; } }

    public GameObject guiPause;
    public GameObject btnPause;

    bool showTutorial { get { return countPlay < 3; } }
    public GameObject tutorial;

    protected override void Awake()
    {
        base.Awake();
        _instance = this;
        tutorial.SetActive(false);
    }
    protected override void init()
    {
        countPlay = 0;
        listFood = new List<Food>();
        StartCoroutine(UpdateTime());
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
        countPlay++;
        tutorial.SetActive(showTutorial);
        int _index = GamePreferences.Instance.setting.currentDifficulty;
        GameObject _obj = Utils.Spawn(pfHero[_index].gameObject);
        hero = _obj.GetComponent<Control>();
        OnResumeGame();
        time = 0;
        star = 0;
        SpawnFood();
    }
    public void OnPauseGame()
    {
        state = PlayState.pause;
        Time.timeScale = 0;
        guiPause.SetActive(true);
        btnPause.SetActive(false);
    }
    public void OnResumeGame()
    {
        state = PlayState.play;
        Time.timeScale = 1;
        guiPause.SetActive(false);
        btnPause.SetActive(true);
    }
    public void OnOverGame()
    {
        for (int i = 0; i < listFood.Count; i++)
        {
            ObjectPoolManager.Unspawn(listFood[i].gameObject);
        }
        listFood = new List<Food>();
        Destroy(hero.gameObject);
        hero = null;
        GamePreferences.Instance.setting.updateScore(star, time);
        GamePreferences.Instance.setting.updateCoin(star);
        GamePreferences.Instance.SaveSetting();
        GameStatesManager.Instance.stateMachine.PushState(GSResult.Instance);
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
    void SpawnFood()
    {
        _posSpawn.x = Random.Range(Pos.spawnLeft, Pos.spawnRight);
        _posSpawn.y = lanes[Random.Range(0, lanes.Length)].position.y;
        _objSpawn = ObjectPoolManager.Spawn(food, _posSpawn, Quaternion.identity);
        _foodSpawn = _objSpawn.GetComponent<Food>();
        listFood.Add(_foodSpawn);
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

    public void OnClickPause()
    {
        OnPauseGame();
    }
    public void OnClickHome()
    {
        OnOverGame();
    }
    public void OnClickPlay()
    {
        OnResumeGame();
    }
}

public enum PlayState
{
    none,
    play,
    pause,
    over,
}