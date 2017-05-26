using UnityEngine;
using System;

public class GameStatesManager : MonoBehaviour
{
    static GameStatesManager _instance;
    public static GameStatesManager Instance { get { return _instance; } }
    public static bool enableBackKey = true;
    public GameObject InputProcessor { get; set; }
    public static Action onBackKey { get; set; }
    public StateMachine stateMachine;
    public IState defaultState;
    void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        DataManager.Instance.LoadData();
        GamePreferences.Instance.LoadPreferences();
        AudioManager.SetSFXVolume(GamePreferences.Instance.setting.sound ? 1 : 0);
        if (GamePreferences.Instance.setting.enableTutorial == false)
        {
            SpawnManager.Instance.Init();
        }
        stateMachine.PushState(defaultState);
    }
    void Update()
    {
        if (onBackKey != null && Input.GetKeyDown(KeyCode.Escape))
        {
            if (enableBackKey)
            {
                onBackKey();
            }
        }
    }
}