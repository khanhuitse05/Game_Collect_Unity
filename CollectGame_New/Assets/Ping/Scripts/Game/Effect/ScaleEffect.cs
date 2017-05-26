using UnityEngine;
using System.Collections;

public class ScaleEffect : MonoBehaviour {

    public float speed = 1;
    public float min = 0.9f;
    public float max = 1.1f;
    float current;
    void Start()
    {
        current = min;
    }
    void Update()
    {
        current += speed * Time.fixedDeltaTime;
        if ((current > max && speed > 0) || (current < min && speed < 0))
        {
            speed *= -1;
        }
        transform.localScale = new Vector2(current, current);
    }
}
