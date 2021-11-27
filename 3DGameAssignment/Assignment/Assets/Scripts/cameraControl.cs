using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour
{
    float sensX = 20f;
    float sensY = 20f;
    private float mouseX;
    private float mouseY;
    private float rotationX;
    private float rotationY;
    private float multiplier = 0.01f;

    [SerializeField] Transform cam;
    [SerializeField] Transform orientation;
    [SerializeField] wallRun run;
    [SerializeField] Transform trans;

    private void Update()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        //player rotation base on mouse movements
        rotationY += mouseX * sensX * multiplier; 
        rotationX += mouseY * sensY * multiplier;

        trans.transform.localRotation = Quaternion.Euler(0, rotationY, 0);

        rotationX = Mathf.Clamp(rotationX, -90f, 90f);//limit the up down cam rotation to 90 degrees only

        cam.transform.rotation = Quaternion.Euler(-rotationX, rotationY, run.tilt);
        orientation.transform.rotation = Quaternion.Euler(0, rotationY, 0);

    }
}
