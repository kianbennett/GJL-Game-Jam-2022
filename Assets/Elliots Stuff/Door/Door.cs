using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isOpen;
    private Animator anim;
    public TriggerObject trigger;
    private void Start()
    {
        anim = GetComponent<Animator>();
        isOpen = false;
    }
    private void Update()
    {
        if(trigger.active == true && isOpen == false) //Open Door
        {
            anim.SetTrigger("doorOpen");
            isOpen = true;
        }
        if (trigger.active == false && isOpen == true) //Close Door
        {
            anim.SetTrigger("doorClose");
            isOpen = false;
        }
    }
}
