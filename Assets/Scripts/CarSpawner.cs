using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public Transform puntoDeInicio;
    public GameObject[] autosDisponibles;

    private void Start()
    {
        // Si no se ha seleccionado un auto (por menú), lo forzamos por defecto (solo para pruebas).
        if (GameData.autoSeleccionado == -1)
        {
            GameData.autoSeleccionado = 1; // Cambia por el índice del auto que quieres probar
        }
        for (int i = 0; i < autosDisponibles.Length; i++)
            autosDisponibles[i].SetActive(false);

        int index = GameData.autoSeleccionado;

        if (puntoDeInicio != null && autosDisponibles.Length > index && index >= 0)
        {
            GameObject auto = autosDisponibles[index];
            auto.SetActive(true);
            auto.transform.position = puntoDeInicio.position;
            auto.transform.rotation = puntoDeInicio.rotation;

            // Asignar cámara
            CameraFollow cam = Camera.main.GetComponent<CameraFollow>();
            if (cam != null)
                cam.carTarget = auto.transform;

            // Asegurar que solo el auto activo tenga el tag "Player"
            auto.tag = "Player";
        }
        else
        {
            Debug.LogWarning("CarSpawner: índice de auto inválido o faltan referencias.");
        }
    }
}
