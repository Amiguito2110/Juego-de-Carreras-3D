using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCar : MonoBehaviour
{
    private float espherePosition = 1.2f;
    public Rigidbody rbEsphere;
    // Start is called before the first frame update
    void Start()
    {
        rbEsphere.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(rbEsphere.transform.position.x, rbEsphere.transform.position.y - espherePosition, rbEsphere.transform.position.z);
    }

    private void FixedUpdate()
    {
        rbEsphere.AddForce(Vector3.forward);
    }
}
