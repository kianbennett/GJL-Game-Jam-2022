using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gears : TriggerReceiver
{
    private Animator anim;
    // public TriggerObject trigger;
    private bool gearsOn;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        gearsOn = false;
    }

    // // Update is called once per frame
    // void Update()
    // {
    //     if(trigger.active == true && gearsOn == false)
    //     {
    //         gearsOn = true;
    //         anim.SetTrigger("gearsOn");
    //     }
    //     if (trigger.active == false && gearsOn == true)
    //     {
    //         gearsOn = false;
    //         anim.SetTrigger("gearsOff");
    //     }
    // }
    public override void Activate()
    {
        if(gearsOn == false)
        {
            gearsOn = true;
            anim.SetTrigger("gearsOn");
        }
    }
    public override void Deactivate()
    {
        if(gearsOn == true)
        {
            gearsOn = false;
            anim.SetTrigger("gearsOff");
        }
    }
}
