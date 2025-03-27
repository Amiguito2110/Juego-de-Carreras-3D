using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartScreen : MonoBehaviour
{
    public void PlayTrack1() {
        SceneManager.LoadScene("Pista1");
    }

    public void PlayTrack2()
    {
        SceneManager.LoadScene("Pista2");
    }

    public void Quit()
    {   Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
