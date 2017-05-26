using UnityEngine;
using System.Collections;

public class SettingPopupComponent : MonoBehaviour {

    CanvasGroup canvasGroup;
    public float timeDelay = 0.2f;
    public float scaleEffect = 3;
    void Start()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        StartCoroutine(FadeIn());

    }
    protected IEnumerator FadeIn()
    {
        canvasGroup.alpha = 0;
        transform.localScale = new Vector3(scaleEffect, scaleEffect, scaleEffect);
        float scaleOffset = scaleEffect - 1;
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.fixedDeltaTime / timeDelay;
            transform.localScale -= Vector3.one * ((Time.fixedDeltaTime * scaleOffset)/ timeDelay);
            yield return null;
        }
        transform.localScale = new Vector3(1, 1, 1);
        canvasGroup.alpha = 1;
    }
    protected IEnumerator FadeOut()
    {
        float scaleOffset = scaleEffect - 1;
        while (canvasGroup.alpha > 0)
        {
            transform.localScale += Vector3.one * ((Time.fixedDeltaTime * scaleOffset)/ timeDelay);
            canvasGroup.alpha -= Time.fixedDeltaTime / timeDelay;
            yield return null;
        }
        Destroy(gameObject);
    }

    public void OnYesBtnClicked()
    {
        StartCoroutine(FadeOut());
    }
}
