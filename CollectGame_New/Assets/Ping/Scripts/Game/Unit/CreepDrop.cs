using UnityEngine;
using System.Collections;

public class CreepDrop : Creep
{

    public GameObject body;
    public float minGravity;
    public float maxGravity;
    float gravity;
    public float speedRotate;
    Vector2 velocity;
    public override void Init(int _lane, int _pos = 0)
    {
        lane = _lane;
        gravity = Random.Range(minGravity, maxGravity);
        velocity = new Vector2(0, 0);
        if (Random.Range(0, 2) == 0)
        {
            speedRotate *= -1;
        }
    }
    void Update()
    {
        velocity.y = velocity.y + gravity * Time.deltaTime;
        transform.Translate(velocity * Time.deltaTime);
        body.transform.Rotate(Vector3.forward * Time.deltaTime * speedRotate);
        if ((transform.position.y < Pos.bottom))
        {
            ObjectPoolManager.Unspawn(this.gameObject);
        }
    }
}
