using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationController : MonoBehaviour
{
    public Animator anim;
    [SerializeField] PlayerMovement player;
    [SerializeField] wallRun run;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("isMove", true);
        }

        if (!Input.GetKey(KeyCode.W))
        {
            anim.SetBool("isMove", false);
        }
    }
}
