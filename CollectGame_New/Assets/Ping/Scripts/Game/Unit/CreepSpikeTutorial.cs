using UnityEngine;
using System.Collections;

public class CreepSpikeTutorial : MonoBehaviour
{
    [HideInInspector]
    public int lane;
    public float offsetLane = 0.25f;
    protected float laneY()
    {
        return GSGamePlay.Instance.lanes[lane].position.y + offsetLane; ;
    }
    public Transform body;
    public Transform highlight;
    public float speedScale;
    public float timeHighlight;
    float scale;
    public void Init(int _lane, int _pos = 0)
    {
        lane = _lane;
        float _posX = Pos.center.x;
        transform.position = new Vector3(_posX, laneY());
        scale = -1;
        StartCoroutine(OnShow());
    }
    IEnumerator OnShow()
    {
        body.gameObject.SetActive(false);
        highlight.gameObject.SetActive(true);
        yield return new WaitForSeconds(timeHighlight);
        body.gameObject.SetActive(true);
        highlight.gameObject.SetActive(false);
        body.localScale = new Vector2(1, 0);
        while (body.localScale.y > scale)
        {
            body.localScale = new Vector2(1, body.localScale.y - (speedScale * Time.deltaTime));
            yield return new WaitForFixedUpdate();
        }
        body.localScale = new Vector2(1, scale);
    }
    public void OnHide()
    {
        Destroy(gameObject);
    }
}
