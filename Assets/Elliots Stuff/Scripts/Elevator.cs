using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Elevator : TriggerReceiver
{
    // public TriggerObject trigger;
    private Animator anim;
    private bool elevatorStarted;
    private bool elevatorOn;
    private bool elevatorOpen;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        elevatorStarted = false;
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if(trigger.active == true && !elevatorStarted)
    //     {
    //         anim.SetTrigger("on");
    //         elevatorStarted = true;
    //         elevatorOn = true;
    //     }
    //     if(trigger.active == false && elevatorOn == true)
    //     {
    //         anim.enabled = false;
    //         elevatorOn = false;
    //     }
    //     if (trigger.active == true && elevatorOn == false)
    //     {
    //         anim.enabled = true;
    //         elevatorOn = true;
    //     }
    // }
    public override void Activate()
    {
        if(!elevatorStarted)
        {
            anim.SetTrigger("on");
            elevatorStarted = true;
            elevatorOn = true;
        }
        if(elevatorOn == false)
        {
            anim.enabled = true;
            elevatorOn = true;
        }
    }
    public override void Deactivate()
    {
        if(elevatorOn == true)
        {
            anim.enabled = false;
            elevatorOn = false;
        }
    }
    //public void Open()
    //{
    //    if (elevatorOpen == false)
    //    {
    //        elevatorOpen = true;
    //        anim.SetTrigger("open");
    //    }
    //}
    //public void Close()
    //{
    //    if (elevatorOpen == true)
    //    {
    //        elevatorOpen = false;
    //        anim.SetTrigger("close");
    //    }
    //}
    
}
