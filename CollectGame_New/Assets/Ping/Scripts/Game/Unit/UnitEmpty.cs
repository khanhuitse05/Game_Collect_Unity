using UnityEngine;
using System.Collections;
using System;

public class UnitEmpty : Unit {
    public UnitType unitType;
    public override UnitType getTypeUnit()
    {
        return unitType;
    }
}
