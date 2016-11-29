using UnityEngine;
using System.Collections;

public class HeroHard : Control
{
    public override bool isJump { get { return hero.isJump || hero1.isJump; } }
    public override void Init()
    {
        base.Init();
        hero1.Init(this, 1);
    }
    public Hero hero1;
    protected override void JumeUp()
    {
        if (hero.lane == hero1.lane)
        {
            hero.JumeUp();
        }
        else
        {
            hero1.JumeUp();
        }
    }
    protected override void JumeDown()
    {
        if (hero.lane == hero1.lane)
        {
            hero1.JumeDown();
        }
        else
        {
            hero.JumeDown();
        }
    }
    protected override void TurnLeft()
    {
        hero.TurnLeft();
        hero1.TurnLeft();
    }
    protected override void TurnRight()
    {
        hero.TurnRight();
        hero1.TurnRight();
    }
}
