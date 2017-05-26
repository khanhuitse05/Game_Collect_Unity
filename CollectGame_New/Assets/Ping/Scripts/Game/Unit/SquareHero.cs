using UnityEngine;
using System.Collections;

public class SquareHero : Hero {
    public override void Turning()
    {
        base.Turning();
        SetDirection();
    }
    public override void TurnLeft()
    {
        base.TurnLeft();
        SetDirection();
    }
    public override void TurnRight()
    {
        base.TurnRight();
        SetDirection();
    }
    void SetDirection()
    {
        if (velocity.x > 0)
        {
            body.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            body.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
