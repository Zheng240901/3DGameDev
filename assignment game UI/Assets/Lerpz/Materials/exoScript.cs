using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exoScript : MonoBehaviour
{
    public float speed = 1.0f;
    public float rotationSpeed = 100.0f;

    public Animator animator;
    bool jump;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //float translation = Input.GetAxis("Vertical") * speed;
        //float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        ////move 10 meters per second instead of 10 meters per frame
        //translation *= Time.deltaTime;
        //rotation *= Time.deltaTime;

        //transform.Translate(0, 0, translation);
        //transform.Rotate(0, rotation, 0);

        animator.SetBool("idle", true);
        if (Input.GetAxis("Vertical") > 0.1f) //up arrowkey
        {
            animator.SetBool("walk", true);
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        else if (Input.GetAxis("Vertical") < -0.01f) //down arrowkey
                                                     // 0f means not pressing any key. if you put it as -0.1f, it will 
                                                     // move backward in idle mode
        {
            animator.SetBool("walk", true);
            transform.Translate(0, 0, -speed * Time.deltaTime);
        }
        else if (Input.GetAxis("Horizontal") > 0.1f) //turn right
        {
            animator.SetBool("walk", true);
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
        else if (Input.GetAxis("Horizontal") < -0.01f) //turn left
        {
            animator.SetBool("walk", true);
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        }
        else
        {
            animator.SetBool("walk", false);
        }

        jump = Input.GetKey(KeyCode.J);

        if (jump)
        {
            animator.SetBool("jump", true);
            //anim.CrossFade("jump");
            //anim.Play("jump");
            //anim.Blend("jump", 1.0F, 0.3F);
        }
        else
        {
            animator.SetBool("jump", false);
        }
    }
}
