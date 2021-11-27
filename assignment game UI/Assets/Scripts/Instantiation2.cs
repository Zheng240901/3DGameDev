using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiation2 : MonoBehaviour
{
    public GameObject rino;
    private float timer = 0.0f;
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 5)
        {
            Instantiate(rino, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), transform.rotation);
            timer = 0.0f;
        }
    }
}
