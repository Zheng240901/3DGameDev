using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class playerCollision : MonoBehaviour
{
    private bool doorIsOpen = false;
    private float doorTimer = 0.0f;
    private GameObject currentDoor;

    public float doorOpenTime = 3.0f;
    public AudioClip doorOpenSound;
    public AudioClip doorShutSound;
    RawImage mRawImage;

    // Start is called before the first frame update
    void Start()
    {
        mRawImage = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward, Color.red);
        if(Physics.Raycast(transform.position,transform.forward, out hit, 5.0f))
        {
            if(hit.collider.gameObject.tag == "outpostDoor" && doorIsOpen == false && BatteryCollect.charge >= 4)
            {
                currentDoor = hit.collider.gameObject;
                Door(doorOpenSound, true, "doorOpen", currentDoor);
                GameObject.FindGameObjectWithTag("BatteryGUI").GetComponent<RawImage>().enabled = false;
            }
            else if(hit.collider.gameObject.tag == "outpostDoor" && doorIsOpen == false && BatteryCollect.charge < 4)
            {
                TextHints.message = "The door seems to need more power!";
                TextHints.textOn = true;
                GameObject.FindGameObjectWithTag("BatteryGUI").GetComponent<RawImage>().enabled = true;
            }
        }

        if (doorIsOpen)
        {
            doorTimer += Time.deltaTime;
            if (doorTimer > doorOpenTime)
            {
                //ShutDoor();
                Door(doorShutSound, false, "doorClose", currentDoor);
                doorTimer = 0.0f;
            }
        }
    }

    //private void ShutDoor()
    //{
    //    AudioSource audio = GetComponent<AudioSource>();
    //    audio.clip = doorShutSound;
    //    audio.Play();
    //    doorIsOpen = false;
    //    GameObject myOutpost = GameObject.Find("Outpost"); //name in the hierarhy panel
    //    myOutpost.GetComponent<Animation>().Play("doorClose");
    //}

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // find game object "Crosshair at the hierarchy panel
        GameObject crosshairObj = GameObject.Find("Crosshair");
        RawImage crosshair = crosshairObj.GetComponent<RawImage>();

        //if(hit.gameObject.tag == "outpostDoor" && doorIsOpen == false)
        //{
        //    currentDoor = hit.gameObject;
        //    //openDoor();
        //    Door(doorOpenSound, true, "doorOpen", currentDoor);
        //}

        TextHints.textOn = true;
        TextHints.message = "knock down all 3 at once to win a battery!";
        if (hit.collider == GameObject.Find("mat").GetComponent<Collider>())
        {
            crosshair.enabled = true;
            CoconutThrow.canThrow = true;
        }
        else
        {
            crosshair.enabled = false;
            CoconutThrow.canThrow = false;
        }
    }

    void Door(AudioClip aClip, bool openCheck, string animName, GameObject thisDoor)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = aClip;
        audio.Play();
        doorIsOpen = openCheck;
        thisDoor.transform.parent.GetComponent<Animation>().Play(animName);
    }

    //private void openDoor()
    //{
    //    AudioSource audio = GetComponent<AudioSource>();
    //    audio.clip = doorOpenSound;
    //    audio.Play();
    //    doorIsOpen = true;
    //    GameObject myOutpost = GameObject.Find("outpost");
    //    myOutpost.GetComponent<Animation>().Play("doorOpen");
    //}
}
