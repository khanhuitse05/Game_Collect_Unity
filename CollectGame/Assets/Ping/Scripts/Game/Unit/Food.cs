using UnityEngine;
using System.Collections;
using System;

public class Food : Unit {
    public override UnitType getTypeUnit()
    {
        return UnitType.Food;
    }
}
