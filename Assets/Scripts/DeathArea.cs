using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathArea : MonoBehaviour
{
    public GameObject spawnPoint;
    //public GameObject DeathBox;
    public GameObject player;


    // Update is called once per frame
    void Update()
    {
        



    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            other.transform.position = spawnPoint.transform.position;
        }

    }
}
