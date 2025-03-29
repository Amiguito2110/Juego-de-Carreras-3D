using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    public Image fadeImage;       // Arrastra el FadeImage aquí
    public float fadeDuration = 1f;
    public float waitTime = 2.5f;

    void Start()
    {
        StartCoroutine(PlaySplash());
    }

    IEnumerator PlaySplash()
    {
        // Fade In (de negro a transparente)
        yield return StartCoroutine(Fade(1f, 0f));

        // Espera mostrando el logo
        yield return new WaitForSeconds(waitTime);

        // Fade Out (de transparente a negro)
        yield return StartCoroutine(Fade(0f, 1f));

        // Carga el menú
        //
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator Fade(float from, float to)
    {
        float time = 0f;
        Color color = fadeImage.color;

        while (time < fadeDuration)
        {
            float t = time / fadeDuration;
            color.a = Mathf.Lerp(from, to, t);
            fadeImage.color = color;
            time += Time.deltaTime;
            yield return null;
        }

        // Asegurar valor final
        color.a = to;
        fadeImage.color = color;
    }
}
