using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public bool isTesting = false;

    public enum Axel
    {
        Front,
        Rear
    }

    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public GameObject wheelEffectObj;
        public ParticleSystem smokeParticle;
        public Axel axel;
    }

    //public ControlMode control;

    public float maxAcceleration = 30.0f;
    public float brakeAcceleration = 50.0f;

    public float turnSensitivity = 1.0f;
    public float maxSteerAngle = 30.0f;

    public Vector3 _centerOfMass = new Vector3(0, -0.5f, 0);

    public List<Wheel> wheels;

    public float moveInput;
    public float steerInput;

    private Rigidbody carRb;

    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = _centerOfMass;


    }

    void Update()
    {
        GetInputs();
        AnimateWheels();
    }

    void LateUpdate()
    {
        Move();
        Steer();
        Brake();
    }

    void GetInputs()
    {
        /*
            moveInput = Input.GetAxis("Vertical");
            steerInput = Input.GetAxis("Horizontal");
        
        */
        if (!isTesting) // Solo usa el input real si no estamos en pruebas
        {
            moveInput = Input.GetAxis("Vertical");
            steerInput = Input.GetAxis("Horizontal");
        }
    }

    public void Move()
    {
        float torque = moveInput * maxAcceleration;

        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = torque;
        }
    }

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
                    0.6f // suaviza el giro
                );
            }
        }
    }

    public void Brake()
    {
        float brakeForce = 0f;

        if (Input.GetKey(KeyCode.Space))
        {
            brakeForce = 1000f; // Frenado fuerte instantáneo
        }
        else if (Mathf.Approximately(moveInput, 0))
        {
            brakeForce = 500f; // Freno motor automático (leve)
        }

        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.brakeTorque = brakeForce;
        }
    }

    void AnimateWheels()
    {
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
