using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public TriggerObject trigger;
    public bool showCutscene = true;
    public float cutsceneDelay = 0.7f;
    public float cutsceneDuration = 1.2f;
    public Level LevelToReveal;

    private bool isOpen;
    private Animator anim;
    private bool hasPlayedCutscene;

    private void Start()
    {
        anim = GetComponent<Animator>();
        isOpen = false;
    }
    private void Update()
    {
       if (trigger.active == true && isOpen == false) //Open Door
       {
           if(showCutscene && !hasPlayedCutscene)
           {
               CameraController.Instance.StartCutscene(transform.position, cutsceneDelay, cutsceneDuration, delegate { anim.SetTrigger("doorOpen"); });
               hasPlayedCutscene = true;
           }
           else
           {
               anim.SetTrigger("doorOpen");
           }
           if(LevelToReveal) LevelToReveal.ShowLevel(0.75f);
           isOpen = true;
       }
       if (trigger.active == false && isOpen == true) //Close Door
       {
           if(showCutscene && !hasPlayedCutscene)
           {
               CameraController.Instance.StartCutscene(transform.position, cutsceneDelay, cutsceneDuration, delegate { anim.SetTrigger("doorClose"); });
               hasPlayedCutscene = true;
           }
           else
           {
               anim.SetTrigger("doorClose");
           }
           isOpen = false;
       }
    }
}
