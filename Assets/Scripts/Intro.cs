using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    public Image fadeImage;       //Imagen en negro para la transicion
    public float fadeDuration = 1f;
    public float waitTime = 2.5f;

    void Start()
    {
        StartCoroutine(PlaySplash());
    }

    IEnumerator PlaySplash()
    {
        yield return StartCoroutine(Fade(1f, 0f));  // Desvanece la Imagen negra para la transicion
        
        yield return new WaitForSeconds(waitTime);  // Muestra nuestro logo
        
        yield return StartCoroutine(Fade(0f, 1f)); // Vuelve a aparecer la imagen negra
        
        SceneManager.LoadScene("MainMenu"); // Cargamos la escena del Menu
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
        color.a = to;
        fadeImage.color = color;
    }
}
