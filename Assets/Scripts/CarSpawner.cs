using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public Transform puntoDeInicio;
    public GameObject[] autosDisponibles;

    private void Start()
    {   /*
        //----------------------Para Pruebas--------------------
        // Si no se ha seleccionado un auto (por menú), lo forzamos por defecto (solo para pruebas).
        if (GameData.autoSeleccionado == -1)
        {
            GameData.autoSeleccionado = 1; // Cambia por el índice del auto que quieres probar
        }*/

        //Desactiva todos los autos disponibles en la escena si estuvieran activados, para no causar inconvenientes y cargar 2 autos al mismo tiempo
        for (int i = 0; i < autosDisponibles.Length; i++)
            autosDisponibles[i].SetActive(false);

        int index = GameData.autoSeleccionado; //Obtiene el ID del auto seleccionado previamente del usuario que se encuentra en GameData

        if (puntoDeInicio != null && autosDisponibles.Length > index && index >= 0)
        {   //Verifica que Exista un punto de inicio válido en la escena, que índice esté dentro del rango del arreglo de autos disponibles
            //y que e índice no sea negativo.

            GameObject auto = autosDisponibles[index];
            auto.SetActive(true); //Activa el auto seleccionado
            auto.transform.position = puntoDeInicio.position;
            auto.transform.rotation = puntoDeInicio.rotation; //Posiciona en el punto de inicio junto con la rotacion.

            // Asignar a la camara el auto seleccionado
            CameraFollow cam = Camera.main.GetComponent<CameraFollow>();
            if (cam != null)
                cam.carTarget = auto.transform;

            // Asegurar que solo el auto activo tenga el tag "Player" para ser reconocido al pasar la meta
            auto.tag = "Player";
        }
        else
        {
            Debug.LogWarning("CarSpawner: índice de auto inválido o faltan referencias.");
        }
    }
}
