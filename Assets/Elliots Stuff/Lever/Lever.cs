using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private bool leverOn;
    private Animator anim;
    private bool playerInRange;

    private void Start()
    {
        anim = GetComponent<Animator>();
        leverOn = false;
        this.GetComponent<TriggerObject>().active = false;
        playerInRange = false;
    }
    public void PullLever()
    {
        if(leverOn)
        {
            //Turn off lever
            leverOn = false;
            anim.SetTrigger("leverOff");
            this.GetComponent<TriggerObject>().active = false;
        }
        else
        {
            //Turn on lever
            leverOn = true;
            anim.SetTrigger("leverOn");
            this.GetComponent<TriggerObject>().active = true;
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            PullLever();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
