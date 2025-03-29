using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeFromBlack : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float time = 0f;
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        while (time < fadeDuration)
        {
            float t = time / fadeDuration;
            color.a = Mathf.Lerp(1f, 0f, t);
            fadeImage.color = color;
            time += Time.deltaTime;
            yield return null;
        }

        // Asegura opacidad final
        color.a = 0f;
        fadeImage.color = color;

        // Desactiva el objeto para no interferir más
        gameObject.SetActive(false);
    }
}
