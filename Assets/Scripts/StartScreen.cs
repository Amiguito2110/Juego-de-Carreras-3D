using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartScreen : MonoBehaviour
{
    public void PlayTrack1()
    {
        GameData.pistaSeleccionada = 0; // o el ID que tú manejes
        
    }

    public void PlayTrack2()
    {
        GameData.pistaSeleccionada = 1;
        
    }

    public void Quit()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
