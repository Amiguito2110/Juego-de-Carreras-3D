using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartScreen : MonoBehaviour
{
    public void PlayTrack1()
    {
        GameData.pistaSeleccionada = 0; // Asigna el ID de la Pista1, para cargarlo GameData
        
    }
    public void PlayTrack2()
    {
        GameData.pistaSeleccionada = 1; // Asigna el ID de la Pista2, para cargarlo GameData
    }
    public void Quit()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
