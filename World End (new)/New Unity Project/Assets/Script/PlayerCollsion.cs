using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollsion : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "AI")
        {
            //GameObject myAI = GameObject.Find("AI1");
            //myAI.GetComponent<Animation>().Play("Talking");
            AITalking();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "AI")
        {
            AINotTalking();
        }
    }

    //void Door(AudioClip aClip, bool openCheck, string animName, GameObject thisDoor)
    //{
    //    AudioSource audio = GetComponent<AudioSource>();
    //    audio.clip = aClip;
    //    audio.Play();
    //    doorIsOpen = openCheck;
    //    thisDoor.transform.parent.GetComponent<Animation>().Play(animName);
    //}

    private void AITalking()
    {
        GameObject myAI = GameObject.Find("AI1");
        //myAI.GetComponent<Animator>().enabled = true;
        //myAI.GetComponent<Animation>().Play("Talking");
        animator.SetBool("isTalking", true);

    }

    private void AINotTalking()
    {
        //GameObject myAI = GameObject.Find("AI1");
        //myAI.GetComponent<Animation>().Play("Breathing Idle");
        //myAI.GetComponent<Animator>().enabled = false;
        animator.SetBool("isTalking", false);
    }
}
