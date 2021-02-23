using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement pm;

    float x;
    float z;
    float up;
    float down;

    float upIsPressedSince = 0f;

    public Rigidbody rb;

    Vector3 direction;

    public float walkSpeed;
    public float jumpForce;
    public float jetpackHorizontalForce;
    public float jetpackVerticalForce;

    public float gravityValue;
    public float gravityMultiplier;

    public Transform currentPlanet;

    [HideInInspector] public Vector3 gravityDirection;

    public Transform playerRotationGO;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundMask;
    bool isGrounded = false;
    bool walk = false;

    float isGroundedSince = 0;

    bool jumpInput;

    // Start is called before the first frame update
    private void Awake()
    {
        pm = this;
    }

    private void Update()
    {
        if(currentPlanet != null)
        {
            gravityDirection = (currentPlanet.position - transform.position).normalized;

            //transform.rotation = Quaternion.


            //transform.LookAt(currentPlanet);
        }

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        if (Input.GetButton("Shift"))
        {
            up = 1;
            upIsPressedSince += Time.deltaTime;
        }
        else
        {
            up = 0;
            upIsPressedSince = 0f;
        }

        if (Input.GetButton("Control"))
            down = 1;
        else
            down = 0;

        jumpInput = Input.GetButtonDown("Jump");

        if(jumpInput == true && isGrounded == true)
        {
            if(upIsPressedSince > 0.2f)
                rb.AddForce(-gravityDirection * jumpForce * 1.5f, ForceMode.Impulse);
            else
                rb.AddForce(-gravityDirection * jumpForce, ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);

        if(isGrounded == true)
        {
            isGroundedSince += Time.fixedDeltaTime;
        }
        else
        {
            isGroundedSince = 0f;
        }

        if (isGroundedSince >= 0.2f)
            walk = true;
        else
            walk = false;

        //StartCoroutine(IsGroundedUpdateGap(isGrounded));

        rb.AddForce(playerRotationGO.up * up * jetpackHorizontalForce * Time.fixedDeltaTime, ForceMode.Force);
        rb.AddForce(-playerRotationGO.up * down * jetpackHorizontalForce * Time.fixedDeltaTime, ForceMode.Force);

        direction = (playerRotationGO.right * x + playerRotationGO.forward * z).normalized;

        if (walk == true)
            rb.velocity = direction * walkSpeed * Time.fixedDeltaTime;
        else
            rb.AddForce(direction * jetpackVerticalForce * Time.fixedDeltaTime,ForceMode.Force);
        
        rb.AddForce(gravityDirection * gravityValue * gravityMultiplier * Time.fixedDeltaTime, ForceMode.Acceleration);

    }
}
