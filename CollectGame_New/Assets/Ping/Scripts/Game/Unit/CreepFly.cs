using UnityEngine;
using System.Collections;

public class CreepFly : Creep {

    public GameObject body;
    public float minSpeed;
    public float maxSpeed;
    public Animator anim;
    Vector2 velocity;
    bool isDied;
    public override void Init(int _lane, int _pos = 0)
    {
        lane = _lane;
        float _speed = Random.Range(minSpeed, maxSpeed);
        anim.speed = _speed;
        if (_pos == 0)
        {
            body.transform.localScale = new Vector3(1, 1, 1);
            velocity = new Vector2(_speed, 0);
            transform.position = new Vector3(Pos.topLeftOver.x, laneY());
        }
        else
        {
            body.transform.localScale = new Vector3(-1, 1, 1);
            velocity = new Vector2(-_speed, 0);
            transform.position = new Vector3(Pos.botRightOver.x, laneY());
        }
        isDied = false;
    }
    void Update()
    {
        if (isDied)
        {
            return;
        }
        transform.Translate(velocity * Time.deltaTime);
        if ((transform.position.x > Pos.botRightOver.x && velocity.x > 0) || (transform.position.x < Pos.topLeftOver.x && velocity.x < 0))
        {
            isDied = true;
            SpawnManager.Instance.UnSpawnCreep(this);
        }
    }
}
