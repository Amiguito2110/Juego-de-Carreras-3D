using UnityEngine;

public class CheckpointMeta : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡El carro pasó por la meta!");
            // Aquí después contaremos vueltas
        }
    }
}
