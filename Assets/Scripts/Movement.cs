using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float rocketThrust = 1f;
    [SerializeField] float rotation = 1f;
    Rigidbody myRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            myRigidbody.AddRelativeForce(0, rocketThrust * Time.deltaTime, 0);
        }
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotation);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotation);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        myRigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        myRigidbody.freezeRotation = false;
    }
}
