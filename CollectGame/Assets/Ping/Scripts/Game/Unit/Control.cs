using UnityEngine;
using System.Collections;
using System;

public class Control : Unit {

    public Hero hero;

    Vector3 fpos;
    Vector3 lpos;
    float ftime;
    float ltime;
    float dragDistance;

    public virtual bool isJump { get { return hero.isJump; } }

    void Start()
    {
        Init();
    }
    public virtual void Init () {
        dragDistance = Screen.height * 5 / 100;
        hero.Init(this);
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
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                fpos = touch.position;
                lpos = touch.position;
                ftime = Time.time;
                ltime = Time.time;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                lpos = touch.position;
                ltime = Time.time;
                touchPhase();
            }
        }
#endif
    }
    void touchPhase()
    {
        if (Mathf.Abs(lpos.x - fpos.x) > dragDistance || Mathf.Abs(lpos.y - fpos.y) > dragDistance)
        {
            if (Mathf.Abs(lpos.x - fpos.x) > Mathf.Abs(lpos.y - fpos.y))
            {
                if ((lpos.x > fpos.x))
                {
                    TurnRight();
                }
                else
                {
                    TurnLeft();
                }
            }
            else
            {
                if (lpos.y > fpos.y)
                {
                    JumeUp();
                }
                else
                {
                    JumeDown();
                }
            }
        }
        else
        {
            if (ltime - ftime < 0.1 && lpos == fpos)
            {
                JumeUp();
            }
        }
    }

    protected virtual void JumeUp()
    {
        hero.JumeUp();
    }
    protected virtual void JumeDown()
    {
        hero.JumeDown();
    }
    protected virtual void TurnLeft()
    {
        hero.TurnLeft();
    }
    protected virtual void TurnRight()
    {
        hero.TurnRight();
    }
    public override UnitType getTypeUnit()
    {
        return UnitType.Hero;
    }
}
