using UnityEngine;
using System.Collections;

public class AlphaAnim : MonoBehaviour {
    public float speed = 1;
    public float min = 0;
    public float max = 1;
    SpriteRenderer image;
    float current;
    void Start()
    {
        current = min;
        image = gameObject.GetComponent<SpriteRenderer>();
    }
    void Update () {
        current += speed * Time.deltaTime;
        if ((current > max && speed > 0) || (current < min && speed < 0))
        {
            speed *= -1;
        }
        image.color = new Color(image.color.r, image.color.g, image.color.b, current);
    }
}
