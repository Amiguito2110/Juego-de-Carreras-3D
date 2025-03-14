using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCar : MonoBehaviour
{   //Version Actual Funcionando
    public Rigidbody rbEsphere; // Rigidbody de la esfera
    public float speed = 800f; // Velocidad de aceleración
    public float turnSpeed = 10f; // Velocidad de giro
    private float espherePosition = 1.2f; // Offset para la posición del coche
    public float stoppingDrag = 2f; // Resistencia cuando no se presiona nada
    public float normalDrag = 0.1f; // Resistencia normal

    void Start()
    {
        rbEsphere.transform.parent = null; // Separa la esfera del coche
        rbEsphere.drag = normalDrag; // Establece una resistencia baja al inicio
    }

    void Update()
    {
        // Mantener el coche sobre la esfera
        transform.position = new Vector3(rbEsphere.transform.position.x, rbEsphere.transform.position.y - espherePosition, rbEsphere.transform.position.z);
        // Alinear la rotación del auto con la esfera para evitar que quede girado raro
        transform.rotation = Quaternion.Euler(0, rbEsphere.transform.rotation.eulerAngles.y, 0);
    }

    private void FixedUpdate()
    {
        // Entrada del jugador
        float moveInput = Input.GetAxis("Vertical"); // W/S 
        float turnInput = Input.GetAxis("Horizontal"); // A/D 

        if (moveInput != 0) // Si se está moviendo
        {
            rbEsphere.AddForce(transform.forward * moveInput * speed * Time.fixedDeltaTime, ForceMode.Acceleration);
            rbEsphere.drag = normalDrag; // Mantener resistencia baja mientras se mueve
        }
        else // Si no hay entrada, aplicar resistencia para detenerse
        {
            rbEsphere.drag = stoppingDrag; // Aumentar la resistencia para frenar progresivamente
        }

        if (turnInput != 0) // Si se está girando
        {
            rbEsphere.AddTorque(Vector3.up * turnInput * turnSpeed * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
        else // Si no hay entrada de giro, detener la rotación
        {
            rbEsphere.angularVelocity = Vector3.zero; // DETIENE EL GIRO CUANDO NO SE PRESIONA NADA 
        }
    }
}
