using UnityEngine;
using System.Collections;

public class CircleHero : Hero {

    public float speedRotate;
    protected override void MoveEffect()
    {
        base.MoveEffect();
        body.transform.Rotate(Vector3.forward * Time.deltaTime * velocity.x * speedRotate);
    }
}
