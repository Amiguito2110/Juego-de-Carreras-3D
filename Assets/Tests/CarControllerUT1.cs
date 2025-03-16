using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using static UnityEditor.IMGUI.Controls.PrimitiveBoundsHandle;

public class CarControllerUT1
{
    private GameObject car;
    private CarController carController;

    [UnitySetUp] // Usa UnitySetUp para pruebas en PlayMode
    public IEnumerator Setup()
    {
        SceneManager.LoadScene("Pista1"); //Nombre de la escena donde se realizan las pruebas
        yield return null; // Espera a que se cargue la escena

        car = GameObject.Find("Car1");  //Busca el objeto del auto dentro de la escena
        Assert.IsNotNull(car, "No se encontró el objeto Car1 en la escena.");

        carController = car.GetComponent<CarController>(); //Busca el script Car Controller para manipular el auto
        Assert.IsNotNull(carController, "El Car1 no tiene un script de CarController.");
    }
    //Prueba unitaria de el carro avanzando
    [UnityTest]
    public IEnumerator Auto_Avanza_Tecla_Up_Presionada()
    {
        float startZ = car.transform.position.z;

        carController.isTesting = true; // Activar el modo de prueba
        carController.moveInput = 1f; // Aplicar la aceleración manualmente

        yield return new WaitForSeconds(3f); // Esperar más tiempo para notar movimiento

        float finalZ = car.transform.position.z;
        Debug.Log($" Prueba de avance: El auto avanzó de Z={startZ} a Z={finalZ}.");

        Assert.Greater(finalZ, startZ, $"El auto debería moverse hacia adelante, pero se movió de Z={startZ} a Z={finalZ}.");
    }

    //Prueba unitaria de el carro girando a la izquierda
    [UnityTest]
    public IEnumerator Auto_Gira_Izq_Tecla_Left_Presionada()
    {
        float startRotationY = car.transform.eulerAngles.y;

        carController.isTesting = true; // Modo de prueba
        carController.moveInput = 1f; // Primero avanza
        yield return new WaitForSeconds(2f); // Gana velocidad

        carController.steerInput = -1f; // Simula girar a la izquierda
        yield return new WaitForSeconds(3f); // Esperar para ver el giro

        float finalRotationY = car.transform.eulerAngles.y;

        float rotationChange = Mathf.DeltaAngle(startRotationY, finalRotationY);
        Debug.Log($" Prueba de giro izquierda: Cambio de ángulo = {rotationChange}°.");

        Assert.Less(rotationChange, 0f, $"El auto debería girar a la izquierda, pero giró en la dirección contraria (Cambio = {rotationChange}°).");

    }

    //Prueba unitaria de el carro girando a la derecha
    [UnityTest]
    public IEnumerator Auto_Gira_Der_Tecla_Left_Presionada()
    {
        float startRotationY = car.transform.eulerAngles.y;

        carController.isTesting = true; // Modo de prueba
        carController.moveInput = 1f; // Primero avanza
        yield return new WaitForSeconds(2f); // Gana velocidad

        carController.steerInput = 1f; // Simula girar a la derecha
        yield return new WaitForSeconds(2f); // Esperar para ver el giro

        float finalRotationY = car.transform.eulerAngles.y;
        float rotationChange = Mathf.DeltaAngle(startRotationY, finalRotationY);
        Debug.Log($" Prueba de giro derecha: Cambio de ángulo = {rotationChange}°.");

        Assert.Greater(rotationChange, 0f, $"El auto debería girar a la derecha, pero giró en la dirección contraria (Cambio = {rotationChange}°).");
    }

    //Prueba unitaria de el carro retrocediendo
    [UnityTest]
    public IEnumerator Auto_Retrocede_Tecla_Down_Presionada()
    {
        float startZ = car.transform.position.z; // Guarda la posición inicial

        carController.isTesting = true; // Activa modo de prueba
        carController.moveInput = -1f; // Simula presionar la flecha abajo (retroceder)

        yield return new WaitForSeconds(3f); // Esperar para notar el movimiento

        float finalZ = car.transform.position.z; // Toma la posición después de moverse
        Debug.Log($" Prueba de retroceso: El auto se movió de Z={startZ} a Z={finalZ}.");

        Assert.Less(finalZ, startZ, $"El auto debería moverse hacia atrás, pero se movió de Z={startZ} a Z={finalZ}.");
    }
    //Prueba unitaria de el carro permaneciendo estatico si no hay entrada del teclado
    [UnityTest]
    public IEnumerator Auto_Permanece_No_Input()
    {
        carController.isTesting = true; // Activa el modo de prueba
        carController.moveInput = 0f; // No se presiona ninguna tecla

        float startZ = car.transform.position.z; // Guarda la posición inicial
        yield return new WaitForSeconds(3f); 

        float finalZ = car.transform.position.z; // Verifica la posición después del tiempo de espera
        Debug.Log($" Prueba de auto en reposo: Z inicial = {startZ}, Z final = {finalZ}.");

        Assert.AreEqual(startZ, finalZ, 0.1f, "El auto debería quedarse quieto, pero se movió.");
    }

}
