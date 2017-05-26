using UnityEngine;
using System.Collections;

public class CreepSpike : Creep {

    public Transform body;
    public Transform highlight;
    public float speedScale;
    public float timeHighlight;
    public float timeLive;
    float scale;
    public override void Init(int _lane, int _pos = 0)
    {
        lane = _lane;
        float _posX = Random.Range(Pos.spawnLeft, Pos.spawnRight);
        transform.position = new Vector3(_posX, laneY());
        scale = 1;
        if (Random.Range(0, 2) == 0) scale = 0 - scale;
        if (lane == 0 && scale > 0)
        {
            scale = 0 - scale;
        }
        StartCoroutine(OnShow());
        StartCoroutine(OnHide());
    }
    IEnumerator OnShow()
    {
        body.gameObject.SetActive(false);
        highlight.gameObject.SetActive(true);
        yield return new WaitForSeconds(timeHighlight);
        body.gameObject.SetActive(true);
        highlight.gameObject.SetActive(false);
        body.localScale = new Vector2(1, 0);
        if (scale > 0)
        {
            while (body.localScale.y < scale)
            {
                body.localScale = new Vector2(1, body.localScale.y + (speedScale * Time.deltaTime));
                yield return new WaitForFixedUpdate();
            }
        }
        else
        {
            while (body.localScale.y > scale)
            {
                body.localScale = new Vector2(1, body.localScale.y - (speedScale * Time.deltaTime));
                yield return new WaitForFixedUpdate();
            }
        }
        body.localScale = new Vector2(1, scale);
    }
    IEnumerator OnHide()
    {
        yield return new WaitForSeconds(timeLive + timeHighlight);
        if (scale > 0)
        {
            while (body.localScale.y > 0)
            {
                body.localScale = new Vector2(1, body.localScale.y - (speedScale * Time.deltaTime));
                yield return new WaitForFixedUpdate();
            }
        }
        else
        {
            while (body.localScale.y < 0)
            {
                body.localScale = new Vector2(1, body.localScale.y + (speedScale * Time.deltaTime));
                yield return new WaitForFixedUpdate();
            }
        }
        ObjectPoolManager.Unspawn(this.gameObject);
    }
}
