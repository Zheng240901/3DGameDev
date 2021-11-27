using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CoconutWin : MonoBehaviour
{
    public static int targets = 0;
    private bool haveWon = false;
    public AudioClip win;
    public GameObject battery;

    // Update is called once per frame
    void Update()
    {
        if (targets == 3 && haveWon == false)
        {
            targets = 0;
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = win;
            audio.Play();
            Instantiate(battery, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), transform.rotation);
            haveWon = true;
        }
    }
}
