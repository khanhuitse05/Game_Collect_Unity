using UnityEngine;
using System.Collections;
using System;

public enum ItemType
{
    None = 0,
    Shield = 1,
}
public class Item : Unit {
    public override UnitType getTypeUnit()
    {
        return UnitType.Item;
    }
    public ItemType type;
    public int value;
    public float timeLive;
    float countLive;
    void OnEnable()
    {
        countLive = 0;
    }
    void Update()
    {
        countLive += Time.deltaTime;
        if (countLive > timeLive)
        {
            ObjectPoolManager.Unspawn(this.gameObject);
        }
    }
}
