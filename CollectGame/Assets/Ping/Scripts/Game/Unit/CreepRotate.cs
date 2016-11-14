using UnityEngine;
using System.Collections;

public class CreepRotate : Creep {

    public GameObject body;
    public float speedMove;
    public float speedRotate;
    Vector2 velocity;
    void Start()
    {
        Init(2);
    }
    void Init (int _lane) {
        lane = _lane;
        velocity = new Vector2(speedMove, 0);
        if (Random.Range(0,1) == 0)
        {
            transform.position = new Vector3(Pos.topLeft.x, laneY());
        }
        else
        {
            transform.position = new Vector3(Pos.botRight.x, laneY());
        }
    }
	void Update () {
        transform.Translate(velocity * Time.deltaTime);
        body.transform.Rotate(Vector3.forward * Time.deltaTime * velocity.x * speedRotate);
        if (transform.position.x > Pos.botRight.x)
        {
            velocity.x = -speedMove;
        }
        if (transform.position.x < Pos.topLeft.x)
        {
            velocity.x = speedMove;
        }
    }
}
