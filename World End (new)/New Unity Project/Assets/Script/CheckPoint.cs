using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private GameManager gm;
    [SerializeField] private  Transform spawnPoint;
    [SerializeField] private GameObject checkPointDetector;
    [SerializeField] private Material ActivatedColor;
    public bool isCheckActivated = false;

    //public void DoActivate()
    //{
    //    if (!isCheckActivated)
    //    {
    //        isCheckActivated = true;
    //        checkPointDetector.GetComponent<MeshRenderer>().material = ActivatedColor;

    //        PlayerMovement.instance.UpdateSpawnPoint(spawnPoint);
    //    }


    //}
    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isCheckActivated = true;
            gm.lastCheckPoint = transform.position;
            Debug.Log("touch checkpoint");
        }
    }

}
