using UnityEngine;

public class CheckpointMeta : MonoBehaviour
{
    private LapCounter lapCounter; //Guarda una referencia al componente LapCounter el cual encargará de contar las vueltas al cruzar la meta

    private void Start()
    {
        // Busca automáticamente el script en escena, en un objeto que la tenga para guardar su referencia
        lapCounter = FindObjectOfType<LapCounter>(); 
    }
    //Esta funcion se activa cuando el Auto con el tag "Player" entra en la zona de Chekpoint
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡El carro pasó por la meta!");
            // Llama al contador de vueltas
            lapCounter?.CrossedFinishLine(); //Llama a CrossedFinishLine() en el LapCounter, que se encarga de aumentar el número de vueltas y verificar si ya se completaron las 3.
        }
    }
}
