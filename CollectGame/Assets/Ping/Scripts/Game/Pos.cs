﻿using UnityEngine;
using System.Collections;

public class Pos : MonoBehaviour {


    static Pos _instance;
    public static Pos Instance { get { return _instance; } }

    public Transform _topLeft;
    public Transform _botRight;
    public Transform _topLeftOver;
    public Transform _botRightOver;
    public Transform _spawnLeft;
    public Transform _spawnRight;
    public Transform _hero;

    public static Vector3 topLeft { get { return Instance._topLeft.position; } }
    public static Vector3 botRight { get { return Instance._botRight.position; } }
    public static Vector3 topLeftOver { get { return Instance._topLeftOver.position; } }
    public static Vector3 botRightOver { get { return Instance._botRightOver.position; } }
    public static Vector3 hero { get { return Instance._hero.position; } }
    public static float spawnLeft { get{ return Instance._spawnLeft.position.x; } }
    public static float spawnRight { get{ return Instance._spawnRight.position.x; } }

    void Awake()
    {
        _instance = this;
    }
}
