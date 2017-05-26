using UnityEngine;
using System.Collections;
using System;

public class Control : Unit {

    protected Hero hero;

    bool wait;
    Vector3 fpos;
    Vector3 lpos;
    float dragDistance;
    protected bool isMove;

    public virtual bool isJump { get { return hero.isJump; } }

    void Start()
    {
        Init();
    }
    public virtual void Init () {
        isMove = false;
        dragDistance = Screen.height * 5 / 100;
    }
    protected Hero SpawnHero(int index)
    {
        GameObject _obj = Utils.Spawn(SpawnManager.Instance.pfHero[GamePreferences.Instance.customize.index].hero[index], transform);
        Hero _hero = _obj.GetComponent<Hero>();
        _hero.Init(this);
        return _hero;
    }
    void Update()
    {

        if (!GSGamePlay.Instance.isPlay)
        {
            return;
        }
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
        if (GamePreferences.Instance.setting.swipe)
        {
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
    public virtual void JumeUp()
    {
        if (isMove == false)
        {
            isMove = true;
            TurnRight();
        }
        hero.JumeUp();
    }
    public virtual void JumeDown()
    {
        hero.JumeDown();
    }
    public virtual void TurnLeft()
    {
        hero.TurnLeft();
    }
    public virtual void TurnRight()
    {
        hero.TurnRight();
    }
    public override UnitType getTypeUnit()
    {
        return UnitType.Hero;
    }
}
