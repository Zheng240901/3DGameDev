using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHints : MonoBehaviour
{
    public static bool textOn = false;
    public static string message;
    private float timer = 0.0f;
    Text Hint;

    // Start is called before the first frame update
    void Start()
    {
        Hint = GetComponent<Text>();
        timer = 0.0f;
        textOn = false;
        Hint.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(textOn)
        {
            Hint.enabled = true;
            Hint.text = message;
            timer += Time.deltaTime;
        }

        if (timer >= 5)
        {
            textOn = false;
            Hint.enabled = false;
            timer = 0.0f;
        }
    }
}
