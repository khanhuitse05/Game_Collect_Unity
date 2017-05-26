using UnityEngine;
using System.Collections;

public class LightEffect : MonoBehaviour {

    const float time = 1;
    float count;
    SpriteRenderer sprite;
    void Awake()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        count = 0;
        sprite.color = Color.black;
    }
    public void TurnOn()
    {
        count = time;
        sprite.color = Color.white;
    }
    void Update()
    {
        if (count > 0)
        {
            count -= Time.deltaTime;
            if (count <= 0)
            {
                count = 0;
                sprite.color = Color.black;
            }
        }
    }
}
