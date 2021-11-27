using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float groundSpeed;
    public float airSpeed;
    float horizontal;
    float vertical;
    Rigidbody rb;
    Vector3 direction;
    Vector3 slopeMoveDirection;
    public float airMovement = 0.4f;
    public float groundMovement = 10f;

    public float playerHeight = 1.75f;
    public bool isGrounded;
    public float jumpForce = 5f;

    float groundDistance = 0.2f;
    [SerializeField] LayerMask groundMask;

    public float groundDrag =6f;
    public float airDrag = 2f;

    [SerializeField] Transform orientation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // player will stay still and not fall to the ground
    }

    // Update is called once per frame
    void Update()
    {
        //check the bottom of the semi sphere is in contact with the game objects or not
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, 1, 0), groundDistance, groundMask);

        GameInput();
        ControlDrag();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jump();
        }

        slopeMoveDirection = Vector3.ProjectOnPlane(direction, slopeHit.normal);  
    }

    void GameInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        direction = orientation.forward * vertical + orientation.right * horizontal; 
    }

    void jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else 
        {
            rb.drag = airDrag;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }


    RaycastHit slopeHit;
    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 1.75f + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    void MovePlayer()
    {
        if (isGrounded && !OnSlope()) 
        {
            rb.AddForce(direction.normalized * groundSpeed * groundMovement, ForceMode.Acceleration);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * groundSpeed * groundMovement, ForceMode.Acceleration);

        }
        else if (!isGrounded)
        {
            rb.AddForce(direction.normalized * airSpeed * airMovement, ForceMode.Acceleration);
        }

    }
}
