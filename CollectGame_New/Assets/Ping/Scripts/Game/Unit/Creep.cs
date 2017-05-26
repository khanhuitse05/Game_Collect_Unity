using UnityEngine;
using System.Collections;
using System;

public class Creep : Unit {
    public override UnitType getTypeUnit()
    {
        return UnitType.Creep;
    }

    [HideInInspector]
    public int lane;
    public float offsetLane = 0.25f;
    protected float laneY()
    {
        return GSGamePlay.Instance.lanes[lane].position.y + offsetLane; ;
    }
    public virtual void Init(int _lane, int _pos = 0)
    {
    }
}
