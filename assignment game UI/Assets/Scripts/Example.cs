using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Example : MonoBehaviour
{
    //public Animation anim;
    public float speed = 1.0f;
    public float rotationSpeed = 100.0f;
    private CharacterController characterController; //for character controller

    //public Rigidbody rb;
    //public bool OnTheGround = true;
    public AudioClip batteryCollectSound;

    public Animator animator; //animator controller
    bool jump; //animator controller to change state to jump
    private Vector3 moveDirection;
    private Vector3 rotateDirection;
    public float gravity = 9.8f;
    private float vSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animation>();
        animator = GetComponent<Animator>(); //animator controller
        characterController = GetComponent<CharacterController>(); //for character controller
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //anim.CrossFade("idle");
        //Vector3 forward = transform.TransformDirection(Vector3.forward);
        //float curSpeed = speed * Input.GetAxis("Vertical");
        //if (Input.GetAxis("Vertical") > 0.1f || Input.GetAxis("Vertical") < -0.01f)
        //{
        //    anim.CrossFade("run");
        //}
        //characterController.SimpleMove(forward * curSpeed);

        ////if (Input.GetAxis("Vertical") > 0.1f) //up arrowkey
        ////{
        ////    anim.CrossFade("run");
        ////    transform.Translate(0, 0, speed * Time.deltaTime);
        ////}
        ////else if (Input.GetAxis("Vertical") < -0.01f) //down arrowkey
        ////                                             // 0f means not pressing any key. if you put it as -0.1f, it will 
        ////                                             // move backward in idle mode
        ////{
        ////    anim.CrossFade("run");
        ////    transform.Translate(0, 0, -speed * Time.deltaTime);
        ////}
        ////else if (Input.GetAxis("Horizontal") > 0.1f) //turn right
        //if (Input.GetAxis("Horizontal") > 0.1f)
        //{
        //    anim.CrossFade("run");
        //    transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        //}
        //else if (Input.GetAxis("Horizontal") < -0.01f) //turn left
        //{
        //    anim.CrossFade("run");
        //    transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        //}

        if (Input.GetAxis("Vertical") == 00 && Input.GetAxis("Horizontal") == 0) 
        {
            animator.SetBool("isMove", false);
        }
        else
        {
            Move();
        }

        if (Input.GetKey(KeyCode.Z))
        {
            //anim.CrossFade("kick");
            //animator.SetBool("kick", false);
        }

        //jump = Input.GetKey(KeyCode.J);

        //if(jump)
        //{
        //    animator.SetBool("jumpAnim", true);
        //    //anim.CrossFade("jump");
        //    //anim.Play("jump");
        //   // anim.Blend("jump", 1.0F, 0.3F);
        //    //rb.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
        //    //OnTheGround = false;
        //}
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag == "ground")
    //    {
    //        OnTheGround = true;
    //    }
    //}

    private void Move()
    {
        animator.SetBool("isMove", true);
        float moveZ = Input.GetAxis("Vertical");
        float rotation = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(0, 0, moveZ);
        moveDirection *= speed;
        moveDirection = transform.TransformDirection(moveDirection);

        rotateDirection = new Vector3(0, rotation, 0);
        transform.Rotate(this.rotateDirection);

        if (characterController.isGrounded)
        {
            vSpeed = 0;
            if (Input.GetKeyDown("space"))
            {
                animator.SetBool("jumpAnim", true);
                // complete the jump actions
                //replace with animator controller
            }
            else
            {
                animator.SetBool("jumpAnim", false);
            }
        }
        vSpeed -= gravity * Time.deltaTime;
        moveDirection.y = vSpeed;

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "battery")
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = batteryCollectSound;
            audio.Play();
            BatteryCollect.charge++;
            Destroy(other.gameObject);
        }
    }
}
