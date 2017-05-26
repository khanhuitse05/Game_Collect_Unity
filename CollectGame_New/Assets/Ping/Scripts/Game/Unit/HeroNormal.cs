using UnityEngine;
using System.Collections;

public class HeroNormal : Control
{
    public override void Init()
    {
        base.Init();
        int indexSkin = Random.Range(0, 2);
        hero = SpawnHero(indexSkin);
        Utils.Spawn(GSGamePlay.Instance.explodeSpawn).transform.position = hero.transform.position;
    }
}
