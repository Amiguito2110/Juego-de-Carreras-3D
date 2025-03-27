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

    public float maxAcceleration = 30.0f;
    public float brakeAcceleration = 50.0f;
    public float turnSensitivity = 1.0f;
    public float maxSteerAngle = 30.0f;

    public Vector3 _centerOfMass = new Vector3(0, -0.5f, 0);
    public List<Wheel> wheels;

    public float moveInput;
    public float steerInput;

    private Rigidbody carRb;

    // Nueva variable para deshabilitar controles
    public bool isRaceFinished = false;

    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = _centerOfMass;
    }

    void Update()
    {
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
    {
        if (!isTesting)
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
                    0.6f
                );
            }
        }
    }

    public void Brake()
    {
        float brakeForce = 0f;

        if (Input.GetKey(KeyCode.Space))
        {
            brakeForce = 1000f;
        }
        else if (Mathf.Approximately(moveInput, 0))
        {
            brakeForce = 500f;
        }

        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.brakeTorque = brakeForce;
        }
    }

    void ApplyFullBrake()
    {
        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = 0f;
            wheel.wheelCollider.brakeTorque = 2000f;
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
