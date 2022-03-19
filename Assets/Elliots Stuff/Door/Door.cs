using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Door : TriggerReceiver
{
    // public TriggerObject trigger;

    private bool isOpen;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        isOpen = false;
    }
    // private void Update()
    // {
    //    if (trigger.active == true && isOpen == false) //Open Door
    //    {
    //        if(showCutscene && !hasPlayedCutscene)
    //        {
    //             CameraController.Instance.StartCutscene(cutsceneDelay, cutsceneDuration, delegate { 
    //                 foreach(Door Door in GetAllDoors()) Door.Open();
    //             }, GetAllDoors().Select(o => o.transform.position).ToArray());
    //            hasPlayedCutscene = true;
    //        }
    //        else
    //        {
    //            foreach(Door Door in GetAllDoors()) Door.Open();
    //        }
    //        if(LevelToReveal) LevelToReveal.ShowLevel(0.75f);
    //    }
    //    if (trigger.active == false && isOpen == true) //Close Door
    //    {
    //        if(showCutscene && !hasPlayedCutscene)
    //        {
    //            CameraController.Instance.StartCutscene(cutsceneDelay, cutsceneDuration, delegate { 
    //                 foreach(Door Door in GetAllDoors()) Door.Close();
    //             }, GetAllDoors().Select(o => o.transform.position).ToArray());
    //            hasPlayedCutscene = true;
    //        }
    //        else
    //        {
    //            foreach(Door Door in GetAllDoors()) Door.Close();
    //        }
    //    }
    // }
    public override void Activate()
    {
        if(isOpen) return;
        anim.SetTrigger("doorOpen");
        isOpen = true;
    }
    public override void Deactivate()
    {
        if(!isOpen) return;
        anim.SetTrigger("doorClose");
        isOpen = false;
    }
}
