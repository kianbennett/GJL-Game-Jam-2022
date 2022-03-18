using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Door : MonoBehaviour
{
    public Door[] ConnectedDoors;
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
                CameraController.Instance.StartCutscene(cutsceneDelay, cutsceneDuration, delegate { 
                    foreach(Door Door in GetAllDoors()) Door.Open();
                }, GetAllDoors().Select(o => o.transform.position).ToArray());
               hasPlayedCutscene = true;
           }
           else
           {
               foreach(Door Door in GetAllDoors()) Door.Open();
           }
           if(LevelToReveal) LevelToReveal.ShowLevel(0.75f);
       }
       if (trigger.active == false && isOpen == true) //Close Door
       {
           if(showCutscene && !hasPlayedCutscene)
           {
               CameraController.Instance.StartCutscene(cutsceneDelay, cutsceneDuration, delegate { 
                    foreach(Door Door in GetAllDoors()) Door.Close();
                }, GetAllDoors().Select(o => o.transform.position).ToArray());
               hasPlayedCutscene = true;
           }
           else
           {
               foreach(Door Door in GetAllDoors()) Door.Close();
           }
       }
    }
    public void Open() 
    {
        if(isOpen) return;
        anim.SetTrigger("doorOpen");
        isOpen = true;
    }
    public void Close()
    {
        if(!isOpen) return;
        anim.SetTrigger("doorClose");
        isOpen = false;
    }
    private Door[] GetAllDoors()
    {
        List<Door> Doors = new List<Door>();
        Doors.Add(this);
        Doors.AddRange(ConnectedDoors);
        return Doors.ToArray();
    }
}
