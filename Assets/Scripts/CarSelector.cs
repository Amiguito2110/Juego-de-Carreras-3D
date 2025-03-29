using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarSelector : MonoBehaviour
{
    public void SeleccionarAuto(int autoID)
    {
        GameData.autoSeleccionado = autoID;

        string pista = "";

        switch (GameData.pistaSeleccionada)
        {
            case 0: pista = "Pista1"; break;
            case 1: pista = "Pista2"; break;
            default:
                Debug.LogWarning("No se seleccionó una pista válida.");
                return;
        }

        SceneManager.LoadScene(pista);
    }
}
