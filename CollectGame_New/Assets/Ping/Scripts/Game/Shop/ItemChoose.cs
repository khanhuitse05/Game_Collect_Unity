using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemChoose : MonoBehaviour
{
    public Color colorUnLock;
    public Color colorLock;
    public Image[] circle;
    public void Init(bool _unlock)
    {
        if (!_unlock)
        {
            for (int i = 0; i < circle.Length; i++)
            {
                circle[i].color = colorLock;
            }
        }
        else
        {
            for (int i = 0; i < circle.Length; i++)
            {
                circle[i].color = colorUnLock;
            }
        }
        if (Random.Range(0, 2) == 0)
        {
            circle[0].transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            circle[0].transform.localScale = new Vector3(1, 1, 1);
        }
        if (Random.Range(0, 2) == 0)
        {
            circle[1].transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            circle[1].transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public void SetDifficulty(int _dif)
    {
        circle[1].gameObject.SetActive(_dif == 1);
    }
}
