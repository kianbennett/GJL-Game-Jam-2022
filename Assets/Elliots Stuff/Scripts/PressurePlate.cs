using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool active;
    private int playersOn;
    private void Start()
    {
        playersOn = 0;
    }
    private void Update()
    {
        if(playersOn >= 1)
        {
            active = true;
        }
        if (playersOn == 0)
        {
            active = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playersOn++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playersOn--;
        }
    }
}
