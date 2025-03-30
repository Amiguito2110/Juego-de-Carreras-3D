using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float moveSmoothness;
    public float rotSmoothness;

    public Vector3 moveOffset;
    public Vector3 rotOffset;

    public Transform carTarget;

    void FixedUpdate()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        HandleMovement();
        HandleRotation();
    }


    //Calcula la posici�n objetivo de la c�mara en relaci�n al auto usando un offset.
    //Usa Lerp para mover suavemente la c�mara hacia esa posici�n objetivo.
    void HandleMovement()
    {
        Vector3 targetPos = new Vector3();
        targetPos = carTarget.TransformPoint(moveOffset);

        transform.position = Vector3.Lerp(transform.position, targetPos, moveSmoothness * Time.deltaTime);
    }

    void HandleRotation()
    {
        var direction = carTarget.position - transform.position; //Calcula la direcci�n hacia la que debe mirar la c�mara (hacia el auto).
        var rotation = new Quaternion(); //Usa LookRotation para generar una rotaci�n orientada al auto con un peque�o ajuste (rotOffset).

        rotation = Quaternion.LookRotation(direction + rotOffset, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotSmoothness * Time.deltaTime); //Interpola la rotaci�n actual con la deseada usando Lerp para una transici�n suave.
    }

}
