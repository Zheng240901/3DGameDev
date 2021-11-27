using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IKnockback
{
    public float moveSpeed = 6f;
    private float currentMoveSpeed;
    public float horiMovement; //horizontal
    public float verticMovement;// vertical
    public float movementMutiplier = 10f;
    [SerializeField] private float airMutiplier = 0.2f;
    [SerializeField] private float groundDrag = 6f; // to prevent player for sliding when they are no pressing any key
    [SerializeField] private float airDrag = 4f; // to allow player to fall normally while having drag

    [Header ("Jumpping Setting")]
    //[SerializeField] private bool isGrounded;
    //[SerializeField] private float playerHeight = 2f;

    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private float jumpForce = 5f;
    private float jumpPadForce = 1.5f;
    private float raycastLength = 1.5f;
    public static Rigidbody playerRB;
    Vector3 moveDirection;

    //wrecking ball settings
    [SerializeField] private bool isKnockBack = false;

    //Rotation and look
    private float xRotation;
    private float sensitivity = 50f;
    private float sensMultiplier = 1f;

    public Transform playerCam;
    public Transform orientation;

    //private static Transform currentSpawnPoint;

    private GameManager gm;

    // Start is called before the first frame update

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerRB.freezeRotation = true;

        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log(IsGrounded());  
        PlayerInputs();
        Look();
        ControlDrag();

        if(Input.GetKeyDown(jumpKey) && IsGrounded())
        {
            //Debug.Log("jump key is pressed");
            Jump();
        }
    }

    private void FixedUpdate()
    {
        MovePlayers();
    }

    void PlayerInputs()
    {
        if (!isKnockBack)
        {
            horiMovement = Input.GetAxisRaw("Horizontal");
            verticMovement = Input.GetAxisRaw("Vertical");

            moveDirection = transform.forward * verticMovement + transform.right * horiMovement;
        }
        //horiMovement = Input.GetAxisRaw("Horizontal");
        //verticMovement = Input.GetAxisRaw("Vertical");

        //moveDirection = transform.forward * verticMovement + transform.right * horiMovement;
    }

    void MovePlayers()
    {
        if(IsGrounded())
        {
            playerRB.AddForce(moveDirection.normalized * moveSpeed * movementMutiplier, ForceMode.Acceleration);
        }

        else if(!IsGrounded())
        {
            playerRB.AddForce(moveDirection.normalized * moveSpeed * movementMutiplier * airMutiplier, ForceMode.Acceleration);
        }
        
    }

    void ControlDrag()
    {
        if(IsGrounded())
        {
            playerRB.drag = groundDrag;
        }
        else
        {
            playerRB.drag = airDrag;
        }
    }

    void Jump()
    {
        playerRB.AddForce(transform.up * jumpForce , ForceMode.Impulse);
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, raycastLength);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "jumpPad")
        {
            playerRB.AddForce(transform.up * jumpForce * jumpPadForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "jumpPad")
        {
            playerRB.AddForce(transform.up * jumpForce , ForceMode.Impulse);
        }
    }

    IEnumerator RemoveControl()
    {
        isKnockBack = true;
        yield return new WaitForSeconds(2f);
        playerRB.mass = 1;
        isKnockBack = false;
    }

    public void DoKnockback()
    {
        playerRB.mass = 0.1f;
        if (MoveWreakingBall.isSwingingLeft)
        {
            playerRB.AddForce(-3, 1, 0, ForceMode.Impulse);
            //Debug.Log("push left");
        }
        if (MoveWreakingBall.isSwingingRight)
        {
            playerRB.AddForce(3, 1, 0, ForceMode.Impulse);
            //Debug.Log("push right");
        }
        StartCoroutine(RemoveControl());

    }

    private void OnCollisionStay(Collision collsion)
    {
        if (collsion.gameObject.tag == "breakingPlatform")
        {
            Debug.Log("Platform Destory in 3 second");
            //StartCoroutine(Timer());
            Destroy(collsion.gameObject,2f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "death")
        {
            StartCoroutine(RemoveControl());
            transform.position = gm.lastCheckPoint;
            
        }

        if (other.gameObject.tag == "CheckPoint")
        {
            // StartCoroutine(RemoveControl());
            Debug.Log("touch checkpoint");
            gm.lastCheckPoint = transform.position;

        }

    }

    //public static void UpdateSpawnPoint(Transform newSpawnPoint)
    //{
    //    currentSpawnPoint = newSpawnPoint;
    //}

    //IEnumerator Timer()
    //{
    //    yield return new WaitForSeconds(3.0f);
    //    DestroyPlatform();
    //}

    //void DestroyPlatform()
    //{
    //    Destroy(collision.gameObject);
    //}

    private float desiredX;
    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime * sensMultiplier;

        //Find current look rotation
        Vector3 rot = playerCam.transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;

        //Rotate, and also make sure we dont over- or under-rotate.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Perform the rotations
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
        orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
    }
}
