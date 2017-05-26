using UnityEngine;
using System.Collections;
using System;

public class Lane : Unit {
    public override UnitType getTypeUnit()
    {
        return UnitType.Lane;
    }
    public int index;
}
