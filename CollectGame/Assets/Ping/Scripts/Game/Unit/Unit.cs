using UnityEngine;
using System.Collections;

[System.Flags]
public enum UnitType
{
    None = 0,
    Hero = 1 << 0,
    Creep = 1 << 1,
    Food = 1 << 2,
    Lane = 1 << 3,
}
public abstract class Unit : MonoBehaviour {

    public abstract UnitType getTypeUnit();
}
