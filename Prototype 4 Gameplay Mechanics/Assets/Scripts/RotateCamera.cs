using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    // Variable
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Storing horizontal input to a variable
        float horizontalInput = Input.GetAxis("Horizontal");

        // Applying the rotation according to the horizontal input
        transform.Rotate(Vector3.up, Time.deltaTime * horizontalInput * rotationSpeed);
    }
}
