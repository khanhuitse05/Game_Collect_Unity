using UnityEngine;
using System.Collections;

public class HeroHard : Control
{
    public override bool isJump { get { return hero.isJump || hero1.isJump; } }
    public override void Init()
    {
        base.Init();
        hero = SpawnHero(0);
        hero1 = SpawnHero(1);
        Utils.Spawn(GSGamePlay.Instance.explodeSpawn).transform.position = hero.transform.position;
    }
    protected Hero hero1;
    public override void JumeUp()
    {
        if (isMove == false)
        {
            isMove = true;
            TurnRight();
        }
        if (hero.lane == hero1.lane)
        {
            hero.JumeUp();
        }
        else
        {
            hero1.JumeUp();
        }
    }
    public override void JumeDown()
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
    public override void TurnLeft()
    {
        hero.TurnLeft();
        hero1.TurnLeft();
    }
    public override void TurnRight()
    {
        hero.TurnRight();
        hero1.TurnRight();
    }
}
