﻿using UnityEngine;
using System.Collections;

public class CreepRotate : Creep {

    public GameObject body;
    public float minSpeed;
    public float maxSpeed;
    public float speedRotate;
    Vector2 velocity;
    bool isDied;
    public override void Init (int _lane) {
        lane = _lane;
        float _speed = Random.Range(minSpeed, maxSpeed);
        if (Random.Range(0 , 2) == 0)
        {
            velocity = new Vector2(_speed, 0);
            transform.position = new Vector3(Pos.topLeft.x, laneY());
        }
        else
        {
            velocity = new Vector2(-_speed, 0);
            transform.position = new Vector3(Pos.botRight.x, laneY());
        }
        isDied = false;
    }
	void Update ()
    {
        if (isDied)
        {
            return;
        }
        transform.Translate(velocity * Time.deltaTime);
        body.transform.Rotate(Vector3.forward * Time.deltaTime * velocity.x * speedRotate);
        if ((transform.position.x > Pos.botRightOver.x && velocity.x > 0) || (transform.position.x < Pos.topLeftOver.x && velocity.x < 0))
        {
            SpawnManager.Instance.UnSpawnCreep(this);
        }
    }
}
