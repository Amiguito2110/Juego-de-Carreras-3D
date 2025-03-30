using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public bool isTesting = false;

    //Enum que permite distinguir entre ruedas delanteras y traseras.
    public enum Axel
    {
        Front,
        Rear
    }

    //Estructura que agrupa todos los componentes de una rueda:
    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;//Modelo visual(wheelModel)
        public WheelCollider wheelCollider;//Collider físico(wheelCollider)
        public GameObject wheelEffectObj;//Efectos visuales
        public ParticleSystem smokeParticle;
        public Axel axel;
    }
    //Parámetros para personalizar el comportamiento del vehículo.
    public float maxAcceleration = 30.0f;
    public float brakeAcceleration = 50.0f;
    public float turnSensitivity = 1.0f;
    public float maxSteerAngle = 30.0f;

    public Vector3 _centerOfMass = new Vector3(0, -0.5f, 0); //Ajuste del centro de masa del vehículo para mayor estabilidad.
    public List<Wheel> wheels;

    public float moveInput;
    public float steerInput;

    private Rigidbody carRb;

    // Nueva variable para deshabilitar controles
    public bool isRaceFinished = false; //Se activa al final de la carrera, deshabilitando todos los controles y aplicando freno total.

    void Start()
    {
        //Inicializa el Rigidbody del auto y ajusta su centro de masa para mejorar la estabilidad al conducir.
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = _centerOfMass;
    }

    void Update()
    {   //Solo obtiene inputs y anima las ruedas si la carrera sigue activa
        if (!isRaceFinished)
        {
            GetInputs();
            AnimateWheels();
        }
    }

    void LateUpdate()
    {
        if (!isRaceFinished)
        {
            Move();
            Steer();
            Brake();
        }
        else
        {
            // Frena el auto si ya terminó la carrera
            ApplyFullBrake();
        }
    }

    void GetInputs()
    {   //Lee los ejes de entrada del jugador (teclado o control). Si isTesting está activado, se desactiva la lectura de inputs (útil para test automatizados)
        if (!isTesting)
        {
            moveInput = Input.GetAxis("Vertical");
            steerInput = Input.GetAxis("Horizontal");
        }
    }
    //Aplica torque a todas las ruedas según la entrada vertical (W o S).
    public void Move()
    {
        float torque = moveInput * maxAcceleration;

        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = torque;
        }
    }

    //Aplica giro solo a las ruedas delanteras (definidas con Axel.Front), interpolando para suavizar el ángulo.
    public void Steer()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                float targetAngle = steerInput * turnSensitivity * maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(
                    wheel.wheelCollider.steerAngle,
                    targetAngle,
                    0.6f
                );
            }
        }
    }
    //Aplica diferentes niveles de frenado:
    public void Brake()
    {
        float brakeForce = 0f;

        if (Input.GetKey(KeyCode.Space))
        {
            //Si se presiona barra espaciadora: freno fuerte.
            brakeForce = 1000f;
        }
        else if (Mathf.Approximately(moveInput, 0))
        {
            //Si no se presiona acelerador: freno suave.
            brakeForce = 500f;
        }

        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.brakeTorque = brakeForce;
        }
    }

    void ApplyFullBrake()
    {
        //Se activa al terminar la carrera. Aplica freno completo y elimina torque en todas las ruedas.
        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = 0f;
            wheel.wheelCollider.brakeTorque = 2000f;
        }
    }

    void AnimateWheels()
    {   //Sincroniza la posición y rotación visual del modelo de las ruedas con la física del WheelCollider.
        foreach (var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos, out rot);
            wheel.wheelModel.transform.position = pos;
            wheel.wheelModel.transform.rotation = rot;
        }
    }
}
