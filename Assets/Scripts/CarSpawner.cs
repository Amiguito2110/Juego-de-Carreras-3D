using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public Transform puntoDeInicio; // Referencia al StartPoint
    public GameObject carro;        // El GameObject del carro

    private void Start()
    {
        if (puntoDeInicio != null && carro != null)
        {
            carro.transform.position = puntoDeInicio.position;
            carro.transform.rotation = puntoDeInicio.rotation;
        }
        else
        {
            Debug.LogWarning("CarSpawner: Falta asignar el StartPoint o el carro.");
        }
    }
}
