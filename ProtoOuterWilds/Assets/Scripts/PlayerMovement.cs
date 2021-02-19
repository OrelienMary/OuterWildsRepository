using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float x;
    float z;

    public Rigidbody rb;

    public float speed;

    public float gravityValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        Vector3 direction = (transform.right * x + transform.forward * z).normalized * speed;

        rb.AddForce(direction * speed * Time.fixedDeltaTime,ForceMode.Force);

        rb.AddForce(-Vector3.up * gravityValue * Time.fixedDeltaTime, ForceMode.Force);
    }
}
