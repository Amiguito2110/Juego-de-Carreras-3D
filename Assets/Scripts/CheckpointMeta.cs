using UnityEngine;

public class CheckpointMeta : MonoBehaviour
{
    private LapCounter lapCounter;

    private void Start()
    {
        lapCounter = FindObjectOfType<LapCounter>(); // Busca automáticamente el script en escena
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡El carro pasó por la meta!");
            lapCounter?.CrossedFinishLine(); // Llama al contador de vueltas
        }
    }
}
