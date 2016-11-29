using UnityEngine;
using System.Collections;

public class CreepSpike : Creep {

    public GameObject body;
    public float speedScale;
    public float timeLive;
    float scale;
    public override void Init(int _lane)
    {
        lane = _lane;
        float _posX = Random.Range(Pos.spawnLeft, Pos.spawnRight);
        transform.position = new Vector3(_posX, laneY());
        scale = 1;
        if (Random.Range(0, 2) == 0) scale = 0 - scale;
        StartCoroutine(OnShow());
        StartCoroutine(OnHide());
    }
    IEnumerator OnShow()
    {
        if (scale > 0)
        {
            while (transform.localScale.y < scale)
            {
                transform.localScale = new Vector2(1, transform.localScale.y + (speedScale * Time.deltaTime));
                yield return new WaitForFixedUpdate();
            }
        }
        else
        {
            while (transform.localScale.y > scale)
            {
                transform.localScale = new Vector2(1, transform.localScale.y - (speedScale * Time.deltaTime));
                yield return new WaitForFixedUpdate();
            }
        }
        transform.localScale = new Vector2(1, scale);
    }
    IEnumerator OnHide()
    {
        yield return new WaitForSeconds(timeLive);
        if (scale > 0)
        {
            while (transform.localScale.y > 0)
            {
                transform.localScale = new Vector2(1, transform.localScale.y - (speedScale * Time.deltaTime));
                yield return new WaitForFixedUpdate();
            }
        }
        else
        {
            while (transform.localScale.y < 0)
            {
                transform.localScale = new Vector2(1, transform.localScale.y + (speedScale * Time.deltaTime));
                yield return new WaitForFixedUpdate();
            }
        }
        ObjectPoolManager.Unspawn(this.gameObject);
    }
}
