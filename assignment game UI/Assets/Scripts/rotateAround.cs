using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateAround : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform earth;
    public Transform moon;
    void Start()
    {
        earth = GameObject.FindGameObjectWithTag("earth").transform;
    }

    // Update is called once per frame
    void Update()
    {
        moon.RotateAround(earth.position, Vector3.up, 30 * Time.deltaTime);
    }
}
