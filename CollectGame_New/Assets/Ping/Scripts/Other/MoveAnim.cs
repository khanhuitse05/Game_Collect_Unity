using UnityEngine;
using System.Collections;

public class MoveAnim : MonoBehaviour
{
    public float speed = 1;
    public Transform from;
    public Transform to;
    public Transform triangle;
    public bool horizontal;
    float current;
    Vector3 offset;
    void Start()
    {
        offset = to.position - from.position;
        StartCoroutine(OnSwipe());
    }
    IEnumerator OnSwipe()
    {
        current = 0;
        transform.position = from.position;
        while (current < 1)
        {
            current += speed * Time.fixedDeltaTime;
            transform.position = from.position + (offset * current);
            if (horizontal)
            {
                triangle.localScale = new Vector3(current, 1, 1);
            }
            else
            {
                triangle.localScale = new Vector3(1, current, 1);
            }
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSecondsRealtime(0.5f);
        StartCoroutine(OnSwipe());
    }
}
