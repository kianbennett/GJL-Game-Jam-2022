using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public TriggerObject trigger;
    private Animator anim;
    private bool elevatorStarted;
    private bool elevatorOn;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        elevatorStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(trigger.active == true && !elevatorStarted)
        {
            anim.SetTrigger("on");
            elevatorStarted = true;
            elevatorOn = true;
        }
        if(trigger.active == false && elevatorOn == true)
        {
            anim.enabled = false;
            elevatorOn = false;
        }
        if (trigger.active == true && elevatorOn == false)
        {
            anim.enabled = true;
            elevatorOn = true;
        }
    }
}
