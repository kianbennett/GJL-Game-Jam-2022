using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private bool leverOn;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        leverOn = false;
        this.GetComponent<TriggerObject>().active = false;
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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PullLever();
        }
    }
}
