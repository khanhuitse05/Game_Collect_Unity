using UnityEngine;
using System.Collections;

public class CreepDrop : Creep
{

    public GameObject body;
    public float minSpeed;
    public float maxSpeed;
    public float speedRotate;
    Vector2 velocity;
    public override void Init(int _lane)
    {
        lane = _lane;
        float _speed = Random.Range(minSpeed, maxSpeed);
        velocity = new Vector2(0, _speed);
        float _posX = Random.Range(Pos.spawnLeft, Pos.spawnRight);
        transform.position = new Vector3(_posX, Pos.top);
        if (Random.Range(0, 2) == 0)
        {
            speedRotate *= -1;
        }
    }
    void Update()
    {
        transform.Translate(velocity * Time.deltaTime);
        body.transform.Rotate(Vector3.forward * Time.deltaTime * speedRotate);
        if ((transform.position.y < Pos.bottom))
        {
            ObjectPoolManager.Unspawn(this.gameObject);
        }
    }
}
