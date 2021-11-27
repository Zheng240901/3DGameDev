using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallRun : MonoBehaviour
{
    [SerializeField] Transform orientation;

    public float wallDistance;
    public float minJumpHeight;

    public bool wallLeft = false;
    public bool wallRight = false;

    RaycastHit leftWallHit;
    RaycastHit rightWallHit;

    Rigidbody rb;

    public float wallRunGrav;
    public float wallJumpForce;

    public float normalFov;
    public float wallFov;
    public float midAirFov;
    public float camTilt;
    public float camTiltTime;
    public float tilt { get; private set; }
    [SerializeField]Camera cam;

    bool canWallRun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight);
    }

    void checkWall()
    {
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallDistance);
        wallRight = Physics.Raycast(transform.position,orientation.right, out rightWallHit, wallDistance);

    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        checkWall();

        if (canWallRun())
        {
            if (wallLeft)
            {
                startWallRun();
                Debug.Log("wall runnign on the left");
            }
            else if (wallRight)
            {
                startWallRun();
                Debug.Log("wall runnign on the right");
            }
            else
                stopWallRun();
        }
        else
        {
            stopWallRun();
        }
    }

    void startWallRun()
    {
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, wallFov, midAirFov * Time.deltaTime);

        rb.useGravity = false;

        rb.AddForce(Vector3.down * wallRunGrav, ForceMode.Force); // let the player move on vertical wall

        if (wallLeft)
            tilt = Mathf.Lerp(tilt, -camTilt, camTiltTime * Time.deltaTime);
        else if (wallRight)
            tilt = Mathf.Lerp(tilt, camTilt, camTiltTime * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (wallLeft)
            {
                Vector3 wallRunJumpDirection = transform.up + leftWallHit.normal.normalized;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallJumpForce * 100, ForceMode.Force);
            }
            else if (wallRight)
            {
                Vector3 wallRunJumpDirection = transform.up + rightWallHit.normal.normalized;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallJumpForce * 100, ForceMode.Force);
            }
        }
    }

    void stopWallRun()
    {
        rb.useGravity = true;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, normalFov, midAirFov * Time.deltaTime);
        tilt = Mathf.Lerp(tilt, 0, camTiltTime * Time.deltaTime);
    }
}
