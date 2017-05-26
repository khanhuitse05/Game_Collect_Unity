using UnityEngine;
using System.Collections;

public class TutorialEffect : MonoBehaviour {

    public float time;
    float count;
    public SpriteRenderer sprite;
	void OnEnable() {
        count = time;
        sprite.color = new Color(1, 1, 1, count/time);
    }
    void Update()
    {
        count -= Time.deltaTime;
        if (count <= 0)
        {
            count = 0;
            gameObject.SetActive(false);
        }
        sprite.color = new Color(1, 1, 1, count / time);
    }
}
