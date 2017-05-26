using UnityEngine;
using System.Collections;

public class CreepRotateTutorial : MonoBehaviour {

    [HideInInspector]
    public int lane;
    public float offsetLane = 0.25f;
    float laneY()
    {
        return GSGamePlay.Instance.lanes[lane].position.y + offsetLane; ;
    }
    public GameObject body;
    public float speed;
    public float speedRotate;
    Vector2 velocity;
    public void Init(int _lane, int _pos = 0) {
        lane = _lane;
        if (_pos == 0)
        {
            velocity = new Vector2(speed, 0);
            transform.position = new Vector3(Pos.topLeftOver.x, laneY());
        }
        else
        {
            velocity = new Vector2(-speed, 0);
            transform.position = new Vector3(Pos.botRightOver.x, laneY());
        }
    }
	void Update ()
    {
        transform.Translate(velocity * Time.deltaTime);
        body.transform.Rotate(Vector3.forward * Time.deltaTime * velocity.x * speedRotate);
        if ((transform.position.x > Pos.botRightOver.x && velocity.x > 0) || (transform.position.x < Pos.topLeftOver.x && velocity.x < 0))
        {
            Destroy(gameObject);
        }
    }
}
