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


    //Calcula la posición objetivo de la cámara en relación al auto usando un offset.
    //Usa Lerp para mover suavemente la cámara hacia esa posición objetivo.
    void HandleMovement()
    {
        Vector3 targetPos = new Vector3();
        targetPos = carTarget.TransformPoint(moveOffset);

        transform.position = Vector3.Lerp(transform.position, targetPos, moveSmoothness * Time.deltaTime);
    }

    void HandleRotation()
    {
        var direction = carTarget.position - transform.position; //Calcula la dirección hacia la que debe mirar la cámara (hacia el auto).
        var rotation = new Quaternion(); //Usa LookRotation para generar una rotación orientada al auto con un pequeño ajuste (rotOffset).

        rotation = Quaternion.LookRotation(direction + rotOffset, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotSmoothness * Time.deltaTime); //Interpola la rotación actual con la deseada usando Lerp para una transición suave.
    }

}
